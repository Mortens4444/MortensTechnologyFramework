using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mtf.Utils.Generics;
using Mtf.Utils.StringExtensions;

namespace Mtf.Database
{
    public class DatabaseColumnStructure
    {
        public string ColumnName { get; }

        public string Type { get; }

        public bool Nullable { get; }

        public bool PrimaryKey { get; }

        public bool Identity { get; }

        public int IdentitySeed { get; }

        public int Length { get; }

        public int IdentityIncrement { get; }

        public object DefaultValue { get; }

        private const int NotFound = -1;

        public DatabaseColumnStructure(string columnName, string type, bool nullable = true, bool identity = false, int identitySeed = 1, int identityIncrement = 1, object defaultValue = null, int length = -1, bool primaryKey = false)
        {
            ColumnName = columnName.IndexOf('[') == 0 && columnName.IndexOf(']') == columnName.Length - 1
                ? columnName.Substring(1, columnName.Length - 2)
                : columnName;
            Type = type.ToUpper();
            Nullable = nullable;
            Identity = identity;
            IdentitySeed = identitySeed;
            IdentityIncrement = identityIncrement;
            DefaultValue = GetDefaultValue(defaultValue);
            PrimaryKey = primaryKey;

            Length = length == -1 ? GetTypeLength(type) : length > 8000 ? 8000 : length;
        }

        private object GetDefaultValue(object defaultValue)
        {
            var defaultValueStr = defaultValue.ToString();
            var hasApostrophes = defaultValueStr.StartsWith("'") && defaultValueStr.EndsWith("'");
            return (Type == "NVARCHAR" || Type == "BIT") && !hasApostrophes ? $"'{defaultValue}'" : defaultValue;
        }

        private static int GetTypeLength(string type)
        {
            var typeLengthProvider = new TypeLengthProvider();
            return typeLengthProvider.GetLength(type);
        }

        public static DatabaseColumnStructure GetDatabaseColumnStructure(string columnDescriptor, IEnumerable<string> primaryKeys)
        {
            if (String.IsNullOrEmpty(columnDescriptor))
            {
                return null;
            }
            //columnDescriptor = columnDescriptor.ToUpper();
            var descriptors = columnDescriptor.Split(' ');

            var columnName = descriptors[0];
            if (columnName.IndexOf('[') == 0 && columnName.IndexOf(']') == columnName.Length - 1)
            {
                columnName = columnName.Substring(1, columnName.Length - 2);
            }

            var primaryKey = primaryKeys != null && primaryKeys.Contains(columnName);

            var COLUMN_DESCRIPTOR = columnDescriptor.ToUpper();

            int length;
            var type = descriptors[1];
            bool identity;
            object defaultValue = null;

            var index = type.IndexOf('(');
            if (index > NotFound)
            {
                var lengthStr = type.Substring(index + 1, type.IndexOf(')') - (index + 1));
                length = lengthStr.ToUpper() == "MAX" ? 8000 : Convert.ToInt32(lengthStr);
                type = type.Substring(0, index);
            }
            else
            {
                length = GetTypeLength(type);
            }

            var nullable = COLUMN_DESCRIPTOR.IndexOf("NOT NULL") == NotFound;

            var defaultIndexApostrophe = COLUMN_DESCRIPTOR.IndexOf("DEFAULT '");
            if (COLUMN_DESCRIPTOR.IndexOf("DEFAULT") > NotFound)
            {
                if (defaultIndexApostrophe != NotFound)
                {
                    var endOfStr = columnDescriptor.IndexOf('\'', defaultIndexApostrophe + 10);
                    defaultValue = columnDescriptor.Substring(defaultIndexApostrophe + 8, endOfStr - defaultIndexApostrophe - 7); // Aposztróffal
                    //default_value = column_descriptor.Substring(default_index_apostrophe + 9, end_of_str - default_index_apostrophe - 9);
                }
                else
                {
                    defaultValue = columnDescriptor.Substring("DEFAULT ", " ", true);
                }
            }

            index = COLUMN_DESCRIPTOR.IndexOf("IDENTITY");
            if (index > NotFound)
            {
                identity = true;
                var identitySeed = Convert.ToInt32(COLUMN_DESCRIPTOR.Substring("IDENTITY(", ","));
                var identityIncrement = Convert.ToInt32(COLUMN_DESCRIPTOR.Substring($"IDENTITY({identitySeed},", ")"));
                return new DatabaseColumnStructure(descriptors[0], type, nullable, identity, identitySeed, identityIncrement,
                    defaultValue, length, primaryKey);
            }

            identity = false;
            return new DatabaseColumnStructure(descriptors[0], type, nullable, identity,
                defaultValue: defaultValue, length: length, primaryKey: primaryKey);
        }

        private string GetToString(string columnName)
        {
            // ID BIGINT IDENTITY(1,1) NOT NULL
            var toString = new StringBuilder();
            if (Type != "NVARCHAR" && Type != "BINARY" && Type != "VARBINARY")
            {
                toString.Append($"[{columnName}] {Type}");
            }
            else
            {
                toString.Append(Length == 8000 ? $"[{columnName}] {Type}(MAX)" : $"[{columnName}] {Type}({Length})");
            }

            if (Identity && IsNumberType())
            {
                toString.Append($" IDENTITY({IdentitySeed},{IdentityIncrement})");
            }

            if (!Nullable)
            {
                toString.Append(" NOT");
            }
            toString.Append(" NULL");

            // Only when creating column
            if (!Nullable && DefaultValue != null)
            {
                toString.Append($" DEFAULT {DefaultValue}");
            }

            return toString.ToString();
        }

        private bool IsNumberType()
        {
            return Type == "INT" || Type == "BIGINT" || Type == "SMALLINT" || Type == "TINYINT" || Type == "DECIMAL" || Type == "NUMERIC";
        }

        public override string ToString()
        {
            return GetToString(ColumnName);
        }

        public void ModifyColumnStructure(DatabaseContext dc, string tableName, DatabaseColumnStructure destinationDcs)
        {
            if (this == destinationDcs)
            {
                return;
            }
            // WARNING - This will recreate the original column

            //var def_value_str = DefaultValue.ConvertToString();
            //var def_value = def_value_str.FirstChar() == '\'' && def_value_str.LastChar() == '\'';
            if (DefaultValue != destinationDcs.DefaultValue)
                //if (destination_dcs.DefaultValue != null)
            {
                var constraintNames = DatabaseUtils.GetDefaultConstraintNames(dc, tableName, ColumnName);
                foreach (var constraintName in constraintNames)
                {
                    dc.ExecuteNonQuery($"ALTER TABLE {tableName} DROP CONSTRAINT {constraintName}");
                }

                // Delete old column, create new with default values
                DeleteColumn(dc, tableName);
                CreateColumn(dc, tableName, destinationDcs.ToString());
            }
            else // Modify column type
            {
                ModifyColumn(dc, tableName, destinationDcs);
            }
        }

        public static bool CreateColumn(DatabaseContext dc, string tableName, string columnSpecification)
        {
            return dc.ExecuteNonQuery($"ALTER TABLE {tableName} ADD {columnSpecification};");
        }

        public bool ModifyColumn(DatabaseContext dc, string tableName, DatabaseColumnStructure newColumnStructure)
        {
            if (ToString() != newColumnStructure.ToString())
            {
                // FIXME - We want to add IDENTITY(1,1) property to column
                if (ToString().IndexOf("IDENTITY(1,1) ") == NotFound && newColumnStructure.ToString().IndexOf("IDENTITY(1,1)") > NotFound)
                {
                    /*CREATE TABLE dbo.Tmp_Events (ID bigint NOT NULL IDENTITY (1, 1), language_element_id bigint NOT NULL, note nvarchar(200) NULL, checksum nvarchar(400) NULL) ON [PRIMARY]
                    ALTER TABLE dbo.Tmp_Events SET (LOCK_ESCALATION = TABLE)
                    SET IDENTITY_INSERT dbo.Tmp_Events ON
                    IF EXISTS(SELECT * FROM dbo.Events)
                        EXEC('INSERT INTO dbo.Tmp_Events (ID, language_element_id, note, checksum)
                            SELECT ID, language_element_id, note, checksum FROM dbo.Events WITH (HOLDLOCK TABLOCKX)')
                    SET IDENTITY_INSERT dbo.Tmp_Events OFF
                    DROP TABLE dbo.Events
                    EXECUTE sp_rename N'dbo.Tmp_Events', N'Events', 'OBJECT'
                    ALTER TABLE dbo.Events ADD CONSTRAINT PK__Events__3214EC2707020F21 PRIMARY KEY CLUSTERED (ID) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]*/

                    return false;
                    //return dc.ExecuteNonQuery(String.Format("SET IDENTITY_INSERT {0} ON"), table_name);

                    //dc.ExecuteQuery(String.Format("ALTER TABLE {0} ALTER COLUMN {1};", table_name, new_column_structure.ToString()));
                }
                // FIXME - We want to remove IDENTITY(1,1) property from column
                if (ToString().IndexOf("IDENTITY(1,1) ") > NotFound && newColumnStructure.ToString().IndexOf("IDENTITY(1,1)") == NotFound)
                {
                    /*CREATE TABLE dbo.Tmp_Events (ID bigint NOT NULL, language_element_id bigint NOT NULL, note nvarchar(200) NULL, checksum nvarchar(400) NULL) ON [PRIMARY]
                    ALTER TABLE dbo.Tmp_Events SET (LOCK_ESCALATION = TABLE)
                    IF EXISTS(SELECT * FROM dbo.Events)
                        EXEC('INSERT INTO dbo.Tmp_Events (ID, language_element_id, note, checksum)
                            SELECT ID, language_element_id, note, checksum FROM dbo.Events WITH (HOLDLOCK TABLOCKX)')
                    DROP TABLE dbo.Events
                    EXECUTE sp_rename N'dbo.Tmp_Events', N'Events', 'OBJECT'
                    ALTER TABLE dbo.Events ADD CONSTRAINT PK__Events__3214EC2707020F21 PRIMARY KEY CLUSTERED (ID) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]*/

                    // Create temporary column
                    /*string column_specification = GetToString("_temporary_column_");

                    if (this.default_value != null)
                    {
                        if (this.type == "NVARCHAR") column_specification += String.Format(" DEFAULT '{0}'", this.default_value.ToString());
                        else column_specification += String.Format(" DEFAULT {0}", this.default_value.ToString());
                    }

                    CreateColumn(dc, table_name, column_specification);
                    dc.ExecuteNonQuery(String.Format("UPDATE Events SET _temporary_column_ = {0};", this.column_name));
                    */

                    return false;
                    //return dc.ExecuteNonQuery(String.Format("SET IDENTITY_INSERT {0} OFF", table_name));
                }
                return dc.ExecuteNonQuery($"ALTER TABLE {tableName} ALTER COLUMN {newColumnStructure};");
            }
            return false;
        }

        public bool DeleteColumn(DatabaseContext dc, string tableName)
        {
            return dc.ExecuteNonQuery($"ALTER TABLE {tableName} DROP COLUMN {ColumnName};");
        }

        public static List<DatabaseColumnStructure> GetPrimaryId(IEnumerable<DatabaseColumnStructure> columns)
        {
            return columns.Where(t => t.Identity || t.PrimaryKey).ToList();
        }

        public static DatabaseColumnStructure GetColumnStructure(IEnumerable<DatabaseColumnStructure> columns, string columnName)
        {
            return columns.FirstOrDefault(t => t.ColumnName == columnName);
        }

        public static bool operator ==(DatabaseColumnStructure dcs1, DatabaseColumnStructure dcs2)
        {
            return Equality.IsEqual(dcs1, dcs2);
        }

        public static bool operator !=(DatabaseColumnStructure dcs1, DatabaseColumnStructure dcs2)
        {
            return Equality.IsNotEqual(dcs1, dcs2);
        }

        public override bool Equals(object obj)
        {
            var structure = obj as DatabaseColumnStructure;
            if (structure != null)
            {
                return String.Equals(ToString(), structure.ToString(), StringComparison.CurrentCultureIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Mtf.Utils.Generics;
using Mtf.Utils.StringExtensions;

namespace Mtf.Database
{
    public class DatabaseTableStructure
    {
        private const int NotFound = -1;
        private ICollection<DatabaseColumnStructure> columns;

        public DatabaseTableStructure(string tableName, ICollection<DatabaseColumnStructure> columns, List<DatabaseColumnStructure> primaryIds = null)
        {
            TableName = tableName;
            this.columns = columns ?? new List<DatabaseColumnStructure>();
            PrimaryIds = primaryIds ?? new List<DatabaseColumnStructure>();
        }

        public string TableName { get; }

        public ICollection<DatabaseColumnStructure> Columns => columns;

        public List<DatabaseColumnStructure> PrimaryIds { get; }

        public string GetCommaSeparatedColumnNames()
        {
            return String.Join(", ", Columns.Select(column => column.ColumnName));
        }

        public static DatabaseTableStructure GetDatabaseTableStructure(string tableDescriptor)
        {
            if (String.IsNullOrEmpty(tableDescriptor))
            {
                return null;
            }
            //table_descriptor = table_descriptor.ToUpper();

            var tableName = tableDescriptor.Substring("CREATE TABLE ", " (");
            if (tableName.IndexOf('[') == 0 && tableName.IndexOf(']') == tableName.Length - 1)
            {
                tableName = tableName.Substring(1, tableName.Length - 2);
            }

            var columns = new List<DatabaseColumnStructure>();

            var primaryKeys = tableDescriptor.Substring("PRIMARY KEY(", ")").Split(',');
            for (var i = 0; i < primaryKeys.Length; i++)
            {
                primaryKeys[i] = primaryKeys[i].Trim();
            }

            var index = tableDescriptor.IndexOf('(') + 1;
            var end = false;
            while (!end)
            {
                var index2 = tableDescriptor.IndexOf(", ", index + 1);
                if (index2 == NotFound)
                {
                    end = true;
                    index2 = tableDescriptor.IndexOf(" PRIMARY KEY", index + 1);
                }

                if (index2 != NotFound)
                {
                    var columnDescriptor = tableDescriptor.Substring(index, index2 - index);
                    if (columnDescriptor.IndexOf(", ") == 0)
                    {
                        columnDescriptor = columnDescriptor.Substring(2);
                    }
                    var cs = DatabaseColumnStructure.GetDatabaseColumnStructure(columnDescriptor, primaryKeys);
                    columns.Add(cs);
                }
                index = index2;
            }

            //var columnsArray = (DatabaseColumnStructure[])columns.ToArray(typeof(DatabaseColumnStructure));
            var id = DatabaseColumnStructure.GetPrimaryId(columns);

            if (!id.Any())
            {
                var ids = tableDescriptor.Substring("PRIMARY KEY(", ")").Split(',');
                for (var i = 0; i < ids.Length; i++)
                {
                    for (var j = 0; j < columns.Count; j++)
                    {
                        if (!String.Equals(columns[j].ColumnName, ids[i].Trim(), StringComparison.CurrentCultureIgnoreCase))
                        {
                            continue;
                        }
                        id.Add(columns[j]); // TODO: Why has only one key?
                        break;
                    }
                }
            }

            return new DatabaseTableStructure(tableName, columns, id);
        }

        public IEnumerable<DatabaseColumnStructure> GetPrimaryId()
        {
            return DatabaseColumnStructure.GetPrimaryId(Columns);
        }

        /// <summary>
        /// Create table SQL command.
        /// </summary>
        /// <returns>Ex.: CREATE TABLE Groups (ID bigint IDENTITY(1,1) NOT NULL, parent_id bigint NOT NULL PRIMARY KEY(ID)) ON [PRIMARY];</returns>
        public override string ToString()
        {
            //
            var columnDefinitions = String.Join(", ", Columns.Select(column => column.ToString()));
            var primaryIds = GetPrimaryIdsInStringFormat(PrimaryIds);
            if (PrimaryIds.Count > 0)
            {
                return $"CREATE TABLE [{TableName}] ({columnDefinitions} PRIMARY KEY({primaryIds}) ON [PRIMARY]);";
            }
            return $"CREATE TABLE [{TableName}] ({columnDefinitions});";
        }

        public void ModifyTableStructure(DatabaseContext dc, DatabaseTableStructure destinationDts)
        {
            foreach (var destinationColumnStructure in destinationDts.Columns)
            {
                var dcs = this[destinationColumnStructure.ColumnName];
                if (dcs == null) // This database column must be created
                {
                    DatabaseColumnStructure.CreateColumn(dc, TableName, destinationColumnStructure.ToString());
                }
                else // Check column structures
                {
                    dcs.ModifyColumnStructure(dc, TableName, destinationColumnStructure);
                }
            }
            foreach (var columnStructure in Columns)
            {
                if (destinationDts[columnStructure.ColumnName] == null) // This database column does not exists anymore
                {
                    columnStructure.DeleteColumn(dc, TableName);
                }
            }

            if (!NeedToRecreatePrimaryIds(destinationDts))
            {
                return;
            }

            for (var i = 0; i < PrimaryIds.Count; i++)
            {
                dc.ExecuteNonQuery("DECLARE @CMD VARCHAR(MAX); SET @CMD = 'ALTER TABLE Options DROP CONSTRAINT ' + (SELECT constraint_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1 AND TABLE_NAME = @C AND COLUMN_NAME = @C2); EXEC(@CMD);", TableName, PrimaryIds[i].ColumnName);
            }

            var primaryKeys = GetPrimaryIdsInStringFormat(destinationDts.PrimaryIds);
            var alterTable = $"ALTER TABLE {TableName} ADD PRIMARY KEY ({primaryKeys});";
            dc.ExecuteNonQuery(alterTable);
        }

        private bool NeedToRecreatePrimaryIds(DatabaseTableStructure destinationDts)
        {
            if (PrimaryIds.Count != destinationDts.PrimaryIds.Count)
            {
                return true;
            }

            return destinationDts.PrimaryIds.Any(destinationPrimaryId =>
                PrimaryIds.All(primaryId => destinationPrimaryId.ColumnName != primaryId.ColumnName));
        }

        private static string GetPrimaryIdsInStringFormat(IEnumerable<DatabaseColumnStructure> idColumns)
        {
            return String.Join(", ", idColumns.Select(id => id.ColumnName));
        }

        public bool CreateTable(DatabaseContext dc)
        {
            return dc.ExecuteNonQuery(ToString());
        }

        public bool DeleteTable(DatabaseContext dc)
        {
            return dc.ExecuteNonQuery($"DROP TABLE {TableName};");
        }

        public static bool operator ==(DatabaseTableStructure dts1, DatabaseTableStructure dts2)
        {
            return Equality.IsEqual(dts1, dts2);
        }

        public static bool operator !=(DatabaseTableStructure dts1, DatabaseTableStructure dts2)
        {
            return Equality.IsNotEqual(dts1, dts2);
        }

        public DatabaseColumnStructure this[string columnName]
        {
            get
            {
                return Columns.FirstOrDefault(t => t.ColumnName == columnName);
            }
        }

        public static DatabaseTableStructure operator +(DatabaseTableStructure dts, DatabaseColumnStructure dcs)
        {
            if (dts.columns == null)
            {
                dts.columns = new List<DatabaseColumnStructure>();
            }
            dts.columns.Add(dcs);
            return dts;
        }

        public static DatabaseTableStructure operator -(DatabaseTableStructure dts, DatabaseColumnStructure dcs)
        {
            dts.columns?.Remove(dcs);
            return dts;
        }

        // Equals(object obj, columnOrderIsImportant)
        /*public bool ExactEquals(DatabaseTableStructure other)
        {
            if (other == null)
            {
                return false;
            }

            var myDefinition = ToString().ToUpper();
            var otherTableDefinition = other.ToString().ToUpper();

            if (TableName == other.TableName)
            {
                if (myDefinition != otherTableDefinition)
                {
                    InfoBox.Show($"Tables are not the same: {TableName}", $"{ToString().ToUpper()}{Environment.NewLine}{otherTableDefinition}");
                }
            }
            return myDefinition == otherTableDefinition;
        }*/

        public bool PermissiveEquals(DatabaseTableStructure other)
        {
            if (other != null)
            {
                return TableName == other.TableName && Columns.All(t => t == other[t.ColumnName]);
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            return PermissiveEquals(obj as DatabaseTableStructure);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
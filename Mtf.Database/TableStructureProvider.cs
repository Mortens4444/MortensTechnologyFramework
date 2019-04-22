using System;
using Mtf.Utils.StringExtensions;

namespace Mtf.Database
{
    public class TableStructureProvider
    {
        public DatabaseColumnStructure[] GetDatabaseTableStructure(DatabaseContext dc, string tableName)
        {
            var primaryKeys = dc.ExecuteReader("SELECT name FROM syscolumns WHERE id in (SELECT id FROM sysobjects WHERE name = @table_name) AND colid in (SELECT SIK.colid FROM sysindexkeys SIK JOIN sysobjects SO ON SIK.id = so.id WHERE SIK.indid = 1 AND SO.name = @table_name);", tableName);

            var rows = dc.ExecuteStoredProcedure("sp_columns", "table_name", tableName);
            var result = new DatabaseColumnStructure[rows[0].Length];
            for (var i = 0; i < rows[0].Length; i++)
            {
                var columnName = Convert.ToString(rows[0][i, 3]);
                var length = Convert.ToInt32(rows[0][i, 7]);
                var type = Convert.ToString(rows[0][i, 5]).Remove(" identity").ToUpper();
                if (type == "NVARCHAR" || type == "NTEXT")
                {
                    if (type == "NTEXT" && length == Int32.MaxValue - 1)
                    {
                        type = "NVARCHAR";
                    }
                    length /= 2;
                    if (length > 8000)
                    {
                        length = 8000;
                    }
                }
                var identity = IsIdentity(primaryKeys, columnName);
                var nullable = Convert.ToInt32(rows[0][i, 10]) == 1;
                var defaultValue = rows[0][i, 12] == null ? null : Convert.ToString(rows[0][i, 12]).TrimStart('(').TrimEnd(')');
                result[i] = new DatabaseColumnStructure(columnName, type, nullable, identity, 1, 1, defaultValue, length, identity);
            }

            return result;
        }

        private static bool IsIdentity(ReaderResult primaryKeys, string columnName)
        {
            for (var j = 0; j < primaryKeys.Length; j++)
            {
                if (columnName == Convert.ToString(primaryKeys[j, 0]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
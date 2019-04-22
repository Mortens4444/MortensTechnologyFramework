using System;
using System.Collections.Generic;
using System.Linq;

namespace Mtf.Database
{
    public static class DatabaseUtils
    {
        public static IEnumerable<string> GetDefaultConstraintNames(DatabaseContext dc, string tableName, string columnName)
        {
            var rows = dc.ExecuteReader("SELECT a.name FROM sys.sysobjects a INNER JOIN (SELECT name, id FROM sys.sysobjects WHERE xtype = 'U') b ON (a.parent_obj = b.id) INNER JOIN sys.syscolumns d ON (d.cdefault = a.id) WHERE a.xtype = 'D' AND b.name = @table_name AND d.name = @column_name;", tableName, columnName);
            if (rows.Length == 0)
            {
                return Enumerable.Empty<string>();
            }

            var constraintsNames = new string[rows.Length];
            for (var i = 0; i < rows.Length; i++)
            {
                constraintsNames[i] = Convert.ToString(rows[i, 0]);
            }

            return constraintsNames;
        }
    }
}
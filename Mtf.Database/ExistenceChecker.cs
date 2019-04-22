using System;
using System.Data.SqlClient;

namespace Mtf.Database
{
    public class ExistenceChecker
    {
        public bool IsDatabaseExists(string connectionString)
        {
            var result = false;
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    result = true;
                }
            }
            catch { }
            return result;
        }

        public bool IsTableExists(string connectionString, string table)
        {
            var dc = new DatabaseContext(connectionString);
            var rows = dc.ExecuteReader("SELECT COUNT(TABLE_NAME) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @table", table);
            if (rows.Length != 1)
            {
                throw new Exception("Query should return a single row");
            }
            return Convert.ToInt32(rows[0, 0]) == 1;
        }

        public bool IsDatabaseExists(DatabaseContext dc, string databaseName)
        {
            var rows = dc.ExecuteReader("SELECT DB_ID(@db_name);", databaseName);
            return rows[0, 0] != null;
        }

        public bool IsObjectExists(DatabaseContext dc, string objectName)
        {
            var rows = dc.ExecuteReader("SELECT OBJECT_ID(@object_name);", objectName);
            return rows[0, 0] != null;
        }
    }
}
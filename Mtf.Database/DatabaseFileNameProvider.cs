using System;

namespace Mtf.Database
{
    public class DatabaseFileNameProvider
    {
        private const string QueryShouldReturnAtLeastTwoDataRows = "Query should return at least two data rows";
        private const int ResultRowIndex = 1;
        private const int DatabaseFileIndex = 0;
        private const int LogFileIndex = 1;
        private const int DataIndex = 2;
        private const int ExpectedRowCount = 2;

        public string GetDatabaseFilename(string connectionString, string databaseName)
        {
            var rows = GetHelpDbResult(connectionString, databaseName);
            return rows[ResultRowIndex][DatabaseFileIndex, DataIndex].ToString();
        }

        public string GetDatabaseLogFilename(string connectionString, string databaseName)
        {
            var rows = GetHelpDbResult(connectionString, databaseName);
            return rows[ResultRowIndex][LogFileIndex, DataIndex].ToString();
        }

        public string[] GetDatabaseFilenameAndLogFilename(string connectionString, string databaseName)
        {
            var result = new string[ExpectedRowCount];
            var rows = GetHelpDbResult(connectionString, databaseName);
            result[0] = rows[ResultRowIndex][DatabaseFileIndex, DataIndex].ToString();
            result[1] = rows[ResultRowIndex][LogFileIndex, DataIndex].ToString();
            return result;
        }

        private static SqlReaderResult[] GetHelpDbResult(string connectionString, string databaseName)
        {
            var dbc = new DatabaseContext(connectionString);
            var result = dbc.ExecuteReaders("EXEC sp_helpdb @database_name", databaseName);
            if (result.Length < ExpectedRowCount)
            {
                throw new Exception(QueryShouldReturnAtLeastTwoDataRows);
            }
            return result;
        }
    }
}
using System;
using Mtf.Utils.CharExtensions;
using Mtf.Utils.StringExtensions;

namespace Mtf.Database
{
    public class DatabaseDestroyer
    {
        /// <summary>
        /// Delete database with the specified name
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="databaseName">Name of the database to be delete</param>
        /// <exception cref="System.Data.SqlClient.SqlException">Thrown if the database not exists</exception>
        /// <returns>Returns true if the operation was completed successfully</returns>
        public bool DropDatabase(string connectionString, string databaseName)
        {
            ValidateDatabaseName(databaseName);
            var dc = new DatabaseContext(connectionString);
            return dc.ExecuteNonQuery($"DROP DATABASE {databaseName};");
        }

        private static void ValidateDatabaseName(string databaseName)
        {
            if (databaseName.FirstChar() == '$')
            {
                throw new ArgumentException($"Invalid character in '{databaseName}'. First char cannot be: $");
            }

            for (var i = 0; i < databaseName.Length; i++)
            {
                if (!databaseName[i].IsLetterOrDigit() && !databaseName[i].IsAnyOf('$', '@', '_', '#'))
                {
                    throw new ArgumentException($"Invalid character in '{databaseName} at {i}'.");
                }
            }
        }
    }
}
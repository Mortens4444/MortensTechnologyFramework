using System.IO;
using Mtf.Utils.StringExtensions;

namespace Mtf.Database
{
    public class DatabaseCreator
    {
        public bool CreateDatabase(DatabaseContext dc, string path, string owner, int sizeInKilobytes)
        {
            return CreateDatabase(dc.ConnectionString.Remove(dc.InitialCatalog), path, dc.InitialCatalog, dc.InitialCatalog, owner, sizeInKilobytes);
        }

        public bool CreateDatabase(DatabaseContext dc, string path, string databaseName, string filename, string owner, int sizeInKilobytes)
        {
            return CreateDatabase(dc.ConnectionString.Remove(dc.InitialCatalog), path, databaseName, filename, owner, sizeInKilobytes);
        }

        public bool CreateDatabase(string connectionString, string path, string databaseName, string filename, string owner, int sizeInKilobytes)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                // DO we really need these commands?
                //Utils.GiveFullPermissionToFolder(path, $"SQLServer2005MSSQLUser${SystemInformation.ComputerName}$SQLEXPRESS");
                //Utils.GiveFullPermissionToFolder(path, $"SQLServerMSSQLUser${SystemInformation.ComputerName}$SQLEXPRESS");
            }

            var fullFileName = Path.Combine(path, $"{filename}.mdf");
            var logFullFileName = Path.Combine(path, $"{filename}_log.ldf");
            var createQuery = $"CREATE DATABASE {databaseName}  ON PRIMARY (NAME = {databaseName}, FILENAME = '{fullFileName}', SIZE = {sizeInKilobytes}KB, MAXSIZE = {sizeInKilobytes}KB, FILEGROWTH = 0KB) LOG ON (NAME = {filename}_log, FILENAME = '{logFullFileName}', SIZE = 1024KB, MAXSIZE = 10MB, FILEGROWTH = 10%);";

            var dc = new DatabaseContext(connectionString, 180); // Wait up to 3 minutes
            var result = dc.ExecuteNonQuery(createQuery);
            return result;
        }
    }
}
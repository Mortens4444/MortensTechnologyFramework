namespace Mtf.Utils.StringExtensions
{
    public static class Database
    {
        private const int NotFound = -1;

        // FIXME dc.ExecuteReader("SELECT CASE WHEN SERVERPROPERTY ('EngineEdition') = 4 THEN CASE WHEN (SELECT max_size FROM sys.database_files WHERE file_id = 1) = -1 THEN CASE WHEN CONVERT(REAL, CONVERT(VARCHAR(5), SUBSTRING(CONVERT(VARCHAR(5), SERVERPROPERTY('productversion')), 1, CHARINDEX('.', CONVERT(VARCHAR(5), SERVERPROPERTY('productversion')))-1))) < 10.5 THEN 'You have got ' + CONVERT(VARCHAR(38), (SELECT 4096 - size / 128 FROM sys.database_files WHERE file_id = 1)) + ' MB ' + '(' + CONVERT(VARCHAR(38), CONVERT(int, 100 * CONVERT(float, (SELECT 4096 - size / 128 FROM sys.database_files WHERE file_id = 1)) / 4096)) + '%) available.' + ' You may increase your database size up to 4GB.' ELSE 'You have got ' + CONVERT(VARCHAR(38), (SELECT 10240 - size/128 FROM sys.database_files WHERE file_id = 1)) + ' MB' + '(' + CONVERT(VARCHAR(38), CONVERT(int, 100 * CONVERT(float, (SELECT 10240 - size / 128 FROM sys.database_files WHERE file_id = 1)) / 10240)) + '%) available.' + ' You may increase your database size up to 10GB.' END ELSE CASE WHEN CONVERT(REAL, CONVERT(VARCHAR(5), SUBSTRING(CONVERT(VARCHAR(5), SERVERPROPERTY('productversion')), 1, CHARINDEX('.', CONVERT(VARCHAR(5), SERVERPROPERTY('productversion')))-1))) < 10.5 THEN 'You have got ' + CONVERT(VARCHAR(38), (SELECT max_size - size FROM sys.database_files WHERE file_id = 1) / 128) + ' MB left out of ' + ' MB (' + CONVERT(VARCHAR(38), CONVERT(int, 100 * CONVERT(float, (SELECT max_size - size FROM sys.database_files WHERE file_id = 1)) / CONVERT(float,(SELECT max_size FROM sys.database_files WHERE file_id = 1)))) + '%).' + ' You may increase your database size up to 4GB.' ELSE 'You have got ' + CONVERT(VARCHAR(38), (SELECT max_size - size FROM sys.database_files WHERE file_id = 1) / 128) + ' MB left out of ' + CONVERT(VARCHAR(38), (SELECT max_size FROM sys.database_files WHERE file_id = 1) / 128) + ' MB ('  + CONVERT(VARCHAR(38), CONVERT(int, 100 * CONVERT(float, (SELECT max_size - size FROM sys.database_files WHERE file_id = 1)) / CONVERT(float, (SELECT max_size FROM sys.database_files WHERE file_id = 1)))) + '%).' + ' You may increase your database size up to 10GB.' END END ELSE CASE WHEN (SELECT max_size FROM sys.database_files WHERE file_id = 1) = -1 THEN 'Main file will grow until the disk is full.' ELSE 'You have got ' + CONVERT(VARCHAR(38), (SELECT max_size - size FROM sys.database_files WHERE file_id = 1) / 128) + ' MB left out of ' + CONVERT(VARCHAR(38), (SELECT max_size FROM sys.database_files WHERE file_id = 1) / 128) + ' MB (' + CONVERT(VARCHAR(38), CONVERT(int, 100 * CONVERT(float, (SELECT max_size - size FROM sys.database_files WHERE file_id = 1)) / CONVERT(float, (SELECT max_size FROM sys.database_files WHERE file_id = 1)))) + '%)' END END AS Database_Info");
        public static string RemoveNestedParentheses(this string value)
        {
            if (value.IndexOf("+'('+CAST(") > NotFound)
            {
                value = value.Remove("+'('+CAST(").Remove("ASNVARCHAR)+')'");
            }
            if (value.IndexOf("(SELECT") > NotFound)
            {
                value = value.Remove("(SELECT").Remove("FROM").Remove("WHERE");
            }

            int index_1;
            var db = 0;
            var found = 0;
            while ((index_1 = value.IndexOf('(')) > NotFound)
            {
                var start = index_1;

                int index_2;
                do
                {
                    index_2 = value.IndexOf(')', index_1 + 1);
                    for (var i = index_1; i <= index_2; i++)
                    {
                        switch (value[i])
                        {
                            case '(': db++; break;
                            case ')': if (i != index_1) db--; break;
                        }
                    }
                    //if (db > 0)
                    index_1 = index_2;
                } while (db > 0);
                var end = index_2;

                value = value.Replace(value.Substring(start, end - start + 1), "_" + found);
                found++;
            }
            return value;
        }
    }
}
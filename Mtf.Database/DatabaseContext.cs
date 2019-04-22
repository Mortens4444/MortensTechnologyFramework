using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Mtf.File.Read;
using Mtf.Utils.StringExtensions;

namespace Mtf.Database
{
    public class DatabaseContext
    {
        public bool? MultipleActiveResultSets { get; private set; }

        public string DataSource { get; private set; }

        public int DatabasePort { get; private set; }

        public string InitialCatalog { get; private set; }

        public string UserId { get; private set; }

        public string Password { get; private set; }

        public int CommandTimeout { get; set; }

        public int ConnectionTimeout { get; private set; }

        public bool PersistSecurityInfo { get; private set; }

        public bool IntegratedSecurity { get; private set; }

        private string connectionString;
        private static readonly List<string> sqlDataTypes = new List<string> { "BIGINT", "BIT", "DECIMAL", "INT", "MONEY", "FLOAT", "DATE", "DATETIME2", "DATETIME", "CHAR", "TEXT", "NCHAR", "NTEXT", "BINARY", "IMAGE", "CURSOR", "HIERARCHYID", "SQL_VARIANT", "TABLE" };

        private const int NotFound = -1;

        public DatabaseContext(DatabaseContext dc, string initialCatalog)
        {
            connectionString = dc.ConnectionString;
            GetConnectionDetails(dc.ConnectionString);
            InitialCatalog = initialCatalog;
            connectionString = connectionString.Replace($"={dc.InitialCatalog};", $"={initialCatalog};");
        }

        public DatabaseContext(string ipOrHostname, int port, string initialCatalog, bool integratredSecurity)
            : this($"Data Source={ipOrHostname},{port};Initial Catalog={initialCatalog};Integrated Security={integratredSecurity}")
        { }

        public DatabaseContext(string ipOrHostname, int port, string initialCatalog, bool persistSecurityInfo, bool integratredSecurity)
            : this($"Data Source={ipOrHostname},{port};Initial Catalog={initialCatalog};Persist Security Info={persistSecurityInfo};Integrated Security={integratredSecurity}")
        { }

        public DatabaseContext(string dataSource, string initialCatalog, bool integratredSecurity)
            : this($"Data Source={dataSource};Initial Catalog={initialCatalog};Integrated Security={integratredSecurity}")
        { }

        public DatabaseContext(string dataSource, string initialCatalog, bool persistSecurityInfo, bool integratredSecurity)
            : this($"Data Source={dataSource};Initial Catalog={initialCatalog};Persist Security Info={persistSecurityInfo};Integrated Security={integratredSecurity}")
        { }

        public DatabaseContext(string ipOrHostname, int port, string initialCatalog, string username, string password)
            : this($"Data Source={ipOrHostname},{port};Initial Catalog={initialCatalog};User ID={username};Password={password}")
        { }

        public DatabaseContext(string ipOrHostname, int port, string initialCatalog, string username, string password, int connectionTimeout)
            : this($"Data Source={ipOrHostname},{port};Initial Catalog={initialCatalog};User ID={username};Password={password};Connection Timeout={connectionTimeout}")
        { }

        public DatabaseContext(string xmlConfigurationFileOrConnectionString)
        {
            ConnectionTimeout = 15;
            CommandTimeout = 30;
            if (System.IO.File.Exists(xmlConfigurationFileOrConnectionString))
            {
                var reader = new Reader();
                var xmlConfigurationFileContent = reader.LoadFile(xmlConfigurationFileOrConnectionString);
                for (var i = 0; i < xmlConfigurationFileContent.Length; i++)
                {
                    if (xmlConfigurationFileContent[i].IndexOf("connectionString=") > NotFound)
                    {
                        connectionString = xmlConfigurationFileContent[i].Split('"')[1];
                    }
                }
            }
            else
            {
                connectionString = xmlConfigurationFileOrConnectionString;
            }
            GetConnectionDetails(connectionString);
        }

        public DatabaseContext(string connectionString, int commandTimeout)
        {
            ConnectionTimeout = 15;
            CommandTimeout = commandTimeout;
            this.connectionString = connectionString;
            GetConnectionDetails(this.connectionString);
        }

        private void GetConnectionDetails(string connectionString)
        {
            var values = connectionString.Split('=', ';');
            for (var i = 0; i < values.Length; i += 2)
            {
                switch (values[i].ToLower().Trim())
                {
                    case "data source":
                        if (values[i + 1].IndexOf(',') != NotFound)
                        {
                            DataSource = values[i + 1].Substring(0, values[i + 1].IndexOf(','));
                            DatabasePort = Convert.ToInt32(values[i + 1].Substring(values[i + 1].IndexOf(',') + 1));
                        }
                        else
                        {
                            DataSource = values[i + 1];
                        }
                        break;
                    case "initial catalog":
                    case "database":
                        InitialCatalog = values[i + 1];
                        break;
                    case "user id":
                        UserId = values[i + 1];
                        break;
                    case "password":
                    case "pwd":
                        Password = values[i + 1];
                        break;
                    case "connection timeout":
                        ConnectionTimeout = Convert.ToInt32(values[i + 1]);
                        break;
                    case "integrated security":
                        IntegratedSecurity = Convert.ToBoolean(values[i + 1]);
                        break;
                    case "persist security info":
                        PersistSecurityInfo = Convert.ToBoolean(values[i + 1]);
                        break;
                    case "multipleactiveresultsets":
                        MultipleActiveResultSets = Convert.ToBoolean(values[i + 1]);
                        break;
                }
            }
        }

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
                GetConnectionDetails(value);
            }
        }

        public bool IsDatabaseExists()
        {
            return new ExistenceChecker().IsDatabaseExists(ConnectionString);
        }

        public bool IsTableExists(string table)
        {
            return new ExistenceChecker().IsTableExists(connectionString, table);
        }

        public bool InsertIntoTable(TableDescriptor tableDescriptor, params object[] sqlCommandParameters)
        {
            if (sqlCommandParameters.Length != tableDescriptor.Columns.Length)
            {
                throw new ArgumentException("SqlCommandParameters and tableDescriptor.Columns size must match");
            }

            if (tableDescriptor.TableName.IndexOfAny(new[] {';', '(', ')', ' '}) > NotFound)
            {
                throw new InvalidDataException();
            }

            bool result;
            using (var connection = new SqlConnection(connectionString))
            {
                var sqlParameterNames = new string[sqlCommandParameters.Length];
                for (var i = 0; i < sqlParameterNames.Length; i++)
                {
                    sqlParameterNames[i] = $"@C{i}";
                }

                var queryBuilder = new StringBuilder();
                var queryParametersBuilder = new StringBuilder();
                queryBuilder.Append($"INSERT INTO {tableDescriptor.TableName} (");
                for (var i = 0; i < tableDescriptor.Columns.Length; i++)
                {
                    if (i < tableDescriptor.Columns.Length - 1)
                    {
                        queryBuilder.Append($"{tableDescriptor.Columns[i].ColumnName}, ");
                        queryParametersBuilder.AppendFormat("{0}, ", sqlParameterNames[i]);
                    }
                    else
                    {
                        queryBuilder.Append($"{tableDescriptor.Columns[i].ColumnName}) VALUES (");
                        queryParametersBuilder.Append($"{sqlParameterNames[i]});");
                    }
                }
                queryBuilder.Append(queryParametersBuilder);
                var query = queryBuilder.ToString();

                using (var command = new SqlCommand(query, connection))
                {
                    AddParametersToSqlCommand(command, sqlParameterNames, sqlCommandParameters);
                    connection.Open();
                    command.CommandTimeout = CommandTimeout;
                    command.ExecuteNonQuery();
                    result = true;
                }
                connection.Close();
            }
            return result;
        }

        public bool ExecuteNonQuery(string query)
        {
            bool result;
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.CommandTimeout = CommandTimeout;
                    command.ExecuteNonQuery();
                    result = true;
                }
                connection.Close();
            }
            return result;
        }

        public bool ExecuteNonQuery(string query, params object[] sqlCommandParameters)
        {
            bool result;
            using (var connection = new SqlConnection(connectionString))
            {
                var sqlParameterNames = GetParameterNames(query);
                using (var command = new SqlCommand(query, connection))
                {
                    AddParametersToSqlCommand(command, sqlParameterNames.ToArray(), sqlCommandParameters);
                    connection.Open();
                    command.CommandTimeout = CommandTimeout;
                    command.ExecuteNonQuery();
                    result = true;
                }
            }
            return result;
        }

        public object ExecuteScalar(string query, params object[] sqlCommandParameters)
        {
            object result;
            using (var connection = new SqlConnection(connectionString))
            {
                var sqlParameterNames = GetParameterNames(query);
                using (var command = new SqlCommand(query, connection))
                {
                    AddParametersToSqlCommand(command, sqlParameterNames.ToArray(), sqlCommandParameters);
                    connection.Open();
                    command.CommandTimeout = CommandTimeout;
                    result = command.ExecuteScalar();
                }
                connection.Close();
            }
            return result;
        }

        private static void AddParametersToSqlCommand(SqlCommand command, IList<string> sqlParameterNames, IList<object> sqlCommandParameters)
        {
            if (sqlCommandParameters == null || sqlParameterNames == null) return;

            var i = 0;
            var paramsList = sqlCommandParameters[0] as IList<object>;
            if (paramsList != null && sqlParameterNames.Count > 1 && sqlCommandParameters.Count == 1 && paramsList.Count == sqlParameterNames.Count)
            {
                while (i < sqlParameterNames.Count)
                {
                    command.Parameters.Add(paramsList[i] != null
                        ? new SqlParameter(sqlParameterNames[i], paramsList[i])
                        : new SqlParameter(sqlParameterNames[i], DBNull.Value));
                    i++;
                }

                return;
            }

            if (sqlCommandParameters.Count != sqlParameterNames.Count)
            {
                throw new ArgumentException("SqlCommandParameters and sqlParameterNames size must match");
            }

            while (i < sqlParameterNames.Count)
            {
                command.Parameters.Add(sqlCommandParameters[i] != null
                    ? new SqlParameter(sqlParameterNames[i], sqlCommandParameters[i])
                    : new SqlParameter(sqlParameterNames[i], DBNull.Value));
                i++;
            }
        }

        private static bool ContainsParameter(IEnumerable<string> sqlParameterNames, string parameterName)
        {
            return sqlParameterNames.Any(sqlParameterName => sqlParameterName == parameterName);
        }

        public static bool Is_T_SQL_DataType(string word)
        {
            word = word.Trim().ToUpper();
            return sqlDataTypes.Any(t => word.IndexOf(t) == 0);
        }

        public static List<string> GetDeclaredParameterNames(string query)
        {
            var declaredParameters = new List<string>();
            var words = query.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < words.Length - 2; i++)
            {
                if (words[i].ToUpper() == "DECLARE")
                {
                    bool shouldContinue;
                    do
                    {
                        i++;
                        if (!declaredParameters.Contains(words[i]))
                        {
                            declaredParameters.Add(words[i]);
                        }
                        i++;
                        if (words[i].ToUpper() == "AS")
                        {
                            i++;
                        }
                        shouldContinue = Is_T_SQL_DataType(words[i]) && words[i][words[i].Length - 1] == ',';
                        if (shouldContinue || words[i + 1] != "=")
                        {
                            continue;
                        }

                        i += 2;
                        shouldContinue = words[i][words[i].Length - 1] == ',';
                    }
                    while (shouldContinue);
                }
                i++;
            }
            return declaredParameters;
        }

        public static List<string> GetParameterNames(string query)
        {
            var declaredParameters = GetDeclaredParameterNames(query);
            var sqlParameterNames = new List<string>();

            var startIndex = 0;
            string parameterName;
            while ((parameterName = GetNextParameterName(query, ref startIndex)) != null)
            {
                if (query.Substring(startIndex - 8, 7).Trim().ToUpper() == "DECLARE")
                {
                    if (!declaredParameters.Contains(parameterName))
                    {
                        declaredParameters.Add(parameterName);
                    }
                }
                else
                {
                    if (!declaredParameters.Contains(parameterName) &&
                        !ContainsParameter(sqlParameterNames, parameterName))
                    {
                        sqlParameterNames.Add(parameterName);
                    }
                }
                startIndex++;
            }
            return sqlParameterNames;
        }

        // FIXME
        //DECLARE @id INT, @counter INT; SET @counter = 1; DECLARE ids CURSOR FOR SELECT ID FROM GridsInSequences WHERE sequenceid = @sequence_id ORDER BY number OPEN ids; FETCH NEXT FROM ids INTO @id; WHILE @@FETCH_STATUS = 0 BEGIN UPDATE GridsInSequences SET number = @counter WHERE ID = @id; SET @counter = @counter + 1; FETCH NEXT FROM ids INTO @id; END; CLOSE ids; DEALLOCATE ids;
        private static string GetNextParameterName(string query, ref int startIndex)
        {
            var again = true;
            do
            {
                startIndex = query.IndexOf('@', startIndex);
                if (query[startIndex + 1] == '@')
                {
                    startIndex += 2;
                }
                else
                {
                    again = false;
                }
            } while (again);

            if (startIndex == NotFound)
            {
                return null;
            }

            int length = 0, i = startIndex + 1;
            while (Char.IsLetterOrDigit(query[i]) || query[i] == '_')
            {
                i++;
                length++;
                if (i == query.Length)
                {
                    break;
                }
            }
            if (length == 0)
            {
                throw new ArgumentException("Parameter is zero", nameof(length));
            }
            return query.Substring(startIndex, length + 1);
        }

        public SqlReaderResult ExecuteReader(string query)
        {
            return ExecuteReader(query, null);
        }

        public SqlReaderResult ExecuteReader(string query, params object[] sqlCommandParameters)
        {
            var coloumnNames = GetCommaSeparatedColumnNames(query);
            coloumnNames = CorrectWildcardColumnsIfNecessary(this, query, coloumnNames);

            var result = new SqlReaderResult(coloumnNames);
            using (var connection = new SqlConnection(connectionString))
            {
                var sqlParameterNames = GetParameterNames(query);
                using (var command = new SqlCommand(query, connection))
                {
                    AddParametersToSqlCommand(command, sqlParameterNames.ToArray(), sqlCommandParameters);
                    connection.Open();
                    command.CommandTimeout = CommandTimeout;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GetValues(reader, ref result);
                        }
                    }
                }
                connection.Close();
            }
            return result;
        }

        public SqlReaderResult[] ExecuteReaders(string query)
        {
            return ExecuteReaders(query, null);
        }

        private static void GetValues(IDataRecord reader, ref SqlReaderResult result)
        {
            var values = new object[reader.FieldCount];
            reader.GetValues(values);
            for (var i = 0; i < values.Length; i++)
            {
                if (reader.IsDBNull(i))
                {
                    values[i] = null;
                }
            }
            result.AddRow(values);
        }

        public static string GetCommaSeparatedColumnNames(string query)
        {
            string commaSeparatedColumnNames = null;
            var lastIndex = query.LastIndexOf(" FROM", StringComparison.CurrentCultureIgnoreCase);
            if (query.IndexOf("SELECT ", StringComparison.CurrentCultureIgnoreCase) == 0 && lastIndex > 0)
            {
                commaSeparatedColumnNames = query.Substring(7, lastIndex - 7).Replace(" ", String.Empty);
            }
            return commaSeparatedColumnNames;
        }

        public static string CorrectWildcardColumnsIfNecessary(DatabaseContext dc, string query, string coloumnNames)
        {
            if (coloumnNames == "*")
            {
                var table = query.ToUpper().Substring("FROM ");
                var index = table.IndexOf(' ');
                if (index != NotFound)
                {
                    table = table.Substring(0, index);
                }

                var tableStructureProvider = new TableStructureProvider();
                var tableStructure = new DatabaseTableStructure(table, tableStructureProvider.GetDatabaseTableStructure(dc, table));
                coloumnNames = tableStructure.GetCommaSeparatedColumnNames();
            }
            return coloumnNames;
        }

        public SqlReaderResult[] ExecuteReaders(string query, params object[] sqlCommandParameters)
        {
            var coloumnNames = GetCommaSeparatedColumnNames(query);
            coloumnNames = CorrectWildcardColumnsIfNecessary(this, query, coloumnNames);

            var result = new List<SqlReaderResult>();
            using (var connection = new SqlConnection(connectionString))
            {
                var sqlParameterNames = GetParameterNames(query);
                using (var command = new SqlCommand(query, connection))
                {
                    AddParametersToSqlCommand(command, sqlParameterNames.ToArray(), sqlCommandParameters);
                    connection.Open();
                    command.CommandTimeout = CommandTimeout;
                    using (var reader = command.ExecuteReader())
                    {
                        do
                        {
                            SqlReaderResult subResult = null;
                            while (reader.Read())
                            {
                                if (subResult == null)
                                {
                                    subResult = new SqlReaderResult(coloumnNames);
                                }
                                GetValues(reader, ref subResult);
                            }
                            if (subResult != null)
                            {
                                result.Add(subResult);
                            }
                        } while (reader.NextResult());
                    }
                }
                connection.Close();
            }
            return result.ToArray();
        }

        public SqlReaderResult[] ExecuteStoredProcedure(string storedProcedure)
        {
            return ExecuteStoredProcedure(storedProcedure, null);
        }

        public SqlReaderResult[] ExecuteStoredProcedure(string storedProcedure, params object[] sqlCommandNamesAndParameters)
        {
            object[] sqlCommandParameters = null;
            string[] sqlParameterNames = null;
            if (sqlCommandNamesAndParameters != null)
            {
                if (sqlCommandNamesAndParameters.Length % 2 != 0)
                {
                    throw new ArgumentException("sqlCommandNamesAndParameters length must be even");
                }

                var length = sqlCommandNamesAndParameters.Length/2;
                sqlParameterNames = new string[length];
                sqlCommandParameters = new string[length];
                for (var i = 0; i < sqlCommandNamesAndParameters.Length; i += 2)
                {
                    if (sqlCommandNamesAndParameters[i].GetType() != typeof(string))
                    {
                        throw new ArgumentException("Each even parameter SQL command parameters must be a name with string type");
                    }

                    var j = i/2;
                    sqlParameterNames[j] = sqlCommandNamesAndParameters[i].ToString();
                    sqlCommandParameters[j] = sqlCommandNamesAndParameters[i + 1];
                }
            }

            var result = new List<SqlReaderResult>();
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(storedProcedure, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    AddParametersToSqlCommand(command, sqlParameterNames, sqlCommandParameters);
                    connection.Open();
                    command.CommandTimeout = CommandTimeout;
                    using (var reader = command.ExecuteReader())
                    {
                        do
                        {
                            SqlReaderResult subResult = null;
                            while (reader.Read())
                            {
                                if (subResult == null)
                                {
                                    subResult = new SqlReaderResult();
                                }
                                GetValues(reader, ref subResult);
                            }
                            if (subResult != null)
                            {
                                result.Add(subResult);
                            }
                        }
                        while (reader.NextResult());
                    }
                }
                connection.Close();
            }
            return result.ToArray();
        }

        public override string ToString()
        {
            var parts = connectionString.Split(';');
            return String.Join("  ", parts);
        }
    }
}
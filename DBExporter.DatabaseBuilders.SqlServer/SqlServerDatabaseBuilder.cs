using DBExporter.DatabaseObjects;
using Microsoft.Data.SqlClient;

namespace DBExporter.DatabaseBuilders.SqlServer
{
    public class SqlServerDatabaseBuilder : IExportSourceBuilder
    {
        private readonly string connectionString;

        public SqlServerDatabaseBuilder(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public ExportSource Build(string selectQuery)
        {
            try
            {
                var conn = new SqlConnection(connectionString); // don't use using here, we need to keep it open to read data
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = selectQuery;

                var reader = cmd.ExecuteReader();

                var database = new ExportSource()
                {
                    Connection = conn,
                    Reader = reader,
                };

                return database;
            }
            catch 
            {
                throw;
            }
        }
    }
}

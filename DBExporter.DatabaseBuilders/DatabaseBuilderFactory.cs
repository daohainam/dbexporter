using Azure.Core;
using DBExporter.DatabaseBuilders.SqlServer;
using DBExporter.DatabaseObjects;
using DBExporter.Options;

namespace DBExporter.DatabaseBuilders
{
    public class DatabaseBuilderFactory
    {
        public static IExportSourceBuilder? Connect(ServerTypes serverType, string connectionString) 
        {
            return serverType switch
            {
                ServerTypes.SqlServer => new SqlServerDatabaseBuilder(connectionString),
                _ => null,
            };
        }
    }
}

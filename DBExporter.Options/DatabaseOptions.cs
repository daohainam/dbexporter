namespace DBExporter.Options
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string SelectQuery { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
        public ServerTypes ServerType { get; set; } = ServerTypes.SqlServer;
    }
}

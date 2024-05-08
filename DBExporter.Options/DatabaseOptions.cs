namespace DBExporter.Options
{
    public class DatabaseOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string SelectQuery { get; set; } = string.Empty;
        public string TableNames { get; set; } = string.Empty; // a comma-separated table list
        public ServerTypes ServerType { get; set; } = ServerTypes.SqlServer;
        public bool IsExportFromQuery => string.IsNullOrEmpty(SelectQuery);
    }
}

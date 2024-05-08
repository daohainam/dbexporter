namespace DBExporter.Options
{
    public interface IDatabaseExportOptionsValidator
    {
        void Validate(DatabaseExportOptions options);
    }
}
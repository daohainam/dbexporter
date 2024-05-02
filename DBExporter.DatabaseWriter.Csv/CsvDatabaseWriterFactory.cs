using DBExporter.DatabaseObjects;
using DBExporter.DatabaseWriter.Abstractions;

namespace DBExporter.DatabaseWriter.Csv
{
    public class CsvDatabaseWriterFactory : IDataWriterFactory
    {
        public CsvDatabaseWriterFactory()
        {
        }

        public IDataWriter GetDataWriter()
        {
            return new CsvDataWriter();
        }
    }
}

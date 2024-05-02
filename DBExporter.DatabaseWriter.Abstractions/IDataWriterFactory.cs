namespace DBExporter.DatabaseWriter.Abstractions
{
    public interface IDataWriterFactory
    {
        IDataWriter GetDataWriter();
    }
}

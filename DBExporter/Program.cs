using DBExporter.DatabaseBuilders;
using DBExporter.DatabaseObjects;
using DBExporter.DatabaseWriter.Csv;
using DBExporter.Options;
using System.IO.Compression;

namespace DBExporter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseExportOptions? options = null;

            try
            {
                var optionBuilder = new DatabaseExportOptionsBuilder(args, [SourceOptionsValidator.Instance]);
                options = optionBuilder.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (options == null)
            {
                return;
            }

            var databaseBuilder = DatabaseBuilderFactory.Connect(options.DatabaseOptions.ServerType, options.DatabaseOptions.ConnectionString);
            if (databaseBuilder == null) // this will never happen
            {
                Console.WriteLine("Could not find database builder");
                return;
            }

            try
            {
                using var database = databaseBuilder.Build(options.DatabaseOptions.SelectQuery);
               
                if (options.ExportOptions.ZipCompressed)
                {
                    ExportToZipFile(database, options.ExportOptions);
                }
                else
                {
                    using var stream = new FileStream(options.ExportOptions.FileName, FileMode.Create, FileAccess.Write, FileShare.None);

                    Export(database, stream, options.ExportOptions);

                    stream.Close();
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void ExportToZipFile(ExportSource database, ExportOptions exportOptions)
        {
            using FileStream zipFile = new(exportOptions.FileName, FileMode.Create);
            using (ZipArchive archive = new(zipFile, ZipArchiveMode.Update))
            {
                var entryName = Path.GetFileName(exportOptions.FileName);

                if (Path.GetExtension(entryName).EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                {
                    entryName = Path.GetFileNameWithoutExtension(entryName);
                }

                ZipArchiveEntry zipEntry = archive.CreateEntry(entryName);
                var stream = zipEntry.Open();

                Export(database, stream, exportOptions);

                stream.Flush();
            }
            zipFile.Close();
        }

        private static void Export(ExportSource database, Stream stream, ExportOptions exportOptions)
        {
            var writerFactory = exportOptions.ExportFormats switch
            {
                ExportFormats.Csv => new CsvDatabaseWriterFactory(),
                _ => throw new Exception($"Export format {exportOptions.ExportFormats} not supported"),
            };

            var dataWriter = writerFactory.GetDataWriter();
            dataWriter.WriteData(database, stream);
        }
    }
}

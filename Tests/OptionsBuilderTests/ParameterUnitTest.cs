using DBExporter.Options;

namespace OptionsBuilderTests
{
    public class ParameterUnitTest
    {
        [Theory]
        [InlineData("Server=.;Database=Test", "SELECT * FROM table", "-f:filename", "-server:SqlServer", "-format:csv", "-compress", "-adt")]
        public void ParseValidArgsTest(params string[] args)
        {
            var builder = new DatabaseExportOptionsBuilder(args, [])
            {
                CurrentTimeFunc = () => new DateTime(2024, 05, 01, 23, 59, 59)
            };
            var options = builder.Build(); // no exceptions means no errors

            Assert.NotNull(options);
            Assert.Equal("Server=.;Database=Test", options.DatabaseOptions.ConnectionString);
            Assert.Equal("SELECT * FROM table", options.DatabaseOptions.SelectQuery);
            Assert.Equal(ServerTypes.SqlServer, options.DatabaseOptions.ServerType);
            Assert.Equal(ExportFormats.Csv, options.ExportOptions.ExportFormats);
            Assert.True(options.ExportOptions.ZipCompressed);
            Assert.True(options.ExportOptions.AppendExportTimeToFileName);

            Assert.Equal("filename-20240105-235959.csv.zip", options.ExportOptions.FileName);
        }
    }
}
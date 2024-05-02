using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter.Options
{
    /*
    * Usage: dbexport <connectionstring> <select query> [-f:filename] [-server:<SqlServer>] [-format:<csv|tsql>] [-compress] [-adt]
    */
    public class DatabaseExportOptionsBuilder
    {
        private readonly string[] args;

        public Func<DateTime> CurrentTimeFunc { get; set; } = () => DateTime.Now;
        public string FileDateTimeFormat { get; set; } = "yyyyddMM-HHmmss";

        public DatabaseExportOptionsBuilder(string[] args) { 
            this.args = args;
        }

        public DatabaseExportOptions Build() {
            var options = Parse(args);
            return options;
        }

        protected DatabaseExportOptions Parse(string[] args)
        {
            if (args == null || args.Length < 2)
            {
                throw new ArgumentException("Missing required parameters");
            }

            var options = new DatabaseExportOptions();
            int i = 0;

            options.DatabaseOptions.ConnectionString = args[i++];
            options.DatabaseOptions.SelectQuery = args[i++];

            for (; i < args.Length; i++)
            {
                var arg = args[i];

                if (arg.StartsWith("-f:"))
                {
                    options.ExportOptions.FileName = arg[3..];
                }
                else if (arg.StartsWith("-server:"))
                {
                    var serverTypeName = arg[8..];
                    if ("SqlServer".Equals(serverTypeName, StringComparison.OrdinalIgnoreCase))
                    {
                        options.DatabaseOptions.ServerType = ServerTypes.SqlServer;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException($"Unknown server type: {serverTypeName}");
                    }
                }
                else if (arg.StartsWith("-format:"))
                {
                    var formatName = arg[8..];
                    if ("csv".Equals(formatName, StringComparison.OrdinalIgnoreCase))
                    {
                        options.ExportOptions.ExportFormats = ExportFormats.Csv;
                    }
                    else if ("tsql".Equals(formatName, StringComparison.OrdinalIgnoreCase))
                    {
                        options.ExportOptions.ExportFormats = ExportFormats.TSql;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException($"Unknown export format: {formatName}");
                    }
                }
                else if ("-compress".Equals(arg))
                {
                    options.ExportOptions.ZipCompressed = true;
                }
                else if ("-adt".Equals(arg))
                {
                    options.ExportOptions.AppendExportTimeToFileName = true;
                }
                else
                {
                    throw new ArgumentException($"Unknown option: {arg}");
                }
            }

            if (options.ExportOptions.AppendExportTimeToFileName)
            {
                options.ExportOptions.FileName += $"-{CurrentTimeFunc().ToString(FileDateTimeFormat)}";
            }

            if (options.ExportOptions.ExportFormats == ExportFormats.Csv)
            {
                options.ExportOptions.FileName += ".csv";
            }
            else if (options.ExportOptions.ExportFormats == ExportFormats.TSql)
            {
                options.ExportOptions.FileName += ".sql";
            }

            if (options.ExportOptions.ZipCompressed)
            {
                options.ExportOptions.FileName += ".zip";
            }

            return Validate(options);
        }

        protected DatabaseExportOptions Validate(DatabaseExportOptions options)
        {
            return options;
        }
    }
}

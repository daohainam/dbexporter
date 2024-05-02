using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter.Options
{
    public class DatabaseExportOptions
    {
        public DatabaseOptions DatabaseOptions { get; set; } = new();
        public ExportOptions ExportOptions { get; set; } = new();
    }
}

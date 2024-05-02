using DBExporter.DatabaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter.DatabaseWriter.Abstractions
{
    public interface IDataWriter
    {
        void WriteData(ExportSource database, Stream stream);
    }
}

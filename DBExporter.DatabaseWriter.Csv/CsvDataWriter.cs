using CsvHelper;
using DBExporter.DatabaseObjects;
using DBExporter.DatabaseWriter.Abstractions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBExporter.DatabaseWriter.Csv
{
    internal class CsvDataWriter : IDataWriter
    {
        public CsvDataWriter()
        {
        }

        public void WriteData(ExportSource database, Stream stream)
        {
            using var writer = new StreamWriter(stream, leaveOpen: true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            var columns = database.Reader.GetColumnSchema();
            while (database.Reader.Read())
            {
                for (int i = 0; i < columns.Count; i++)
                {
                    csv.WriteField(database.Reader[i].ToString(), true);
                }

                csv.NextRecord();
            }

            csv.Flush();
        }
    }
}

using System.Data.Common;

namespace DBExporter.DatabaseObjects
{
    public class ExportSource: IDisposable
    {
        private bool disposedValue;

        public required DbConnection Connection { get; set; } // must be a connected, open connection
        public required DbDataReader Reader { get; set; } // must be a connected, open, ready-to-read reader

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!Reader.IsClosed)
                    {
                        Reader.Close();
                    }

                    if (Connection.State == System.Data.ConnectionState.Open)
                    {
                        Connection.Close();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Database()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

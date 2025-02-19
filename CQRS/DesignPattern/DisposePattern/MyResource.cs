namespace CQRS.DesignPattern.DisposePattern
{
    public class MyResource : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            Console.WriteLine("MyResource Dispose(bool disposing)");
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MyResource()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        // Destructor (Finalizer)
        ~MyResource()
        {
            Dispose(false); // Release unmanaged resources if the Dispose method wasn't called explicitly
        }
        void IDisposable.Dispose()
        {
            Console.WriteLine("MyResource Dispose()");
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

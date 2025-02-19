namespace CQRS.DesignPattern.DisposePattern
{
    public class ResourceDerived:MyResource
    {
        private bool _disposedValue = false;
        protected override void Dispose(bool disposing)
        {
            Console.WriteLine("ResourceDerived Dispose(bool disposing)");
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    // For example: Close file handles, database connections, etc.
                }

                // Dispose unmanaged resources
                // For example: Release memory allocated through unmanaged code

                _disposedValue = true;
            }
            // Call base class implementation.
            base.Dispose(disposing);
        }
    }
}

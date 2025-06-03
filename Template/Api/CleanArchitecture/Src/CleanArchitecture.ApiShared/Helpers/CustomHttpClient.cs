namespace CleanArchitecture.ApiShared.Helpers
{
    public class CustomHttpClient : IDisposable
    {
        public HttpClient Client { get; private set; }
        private bool _disposed;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client"></param>
        public CustomHttpClient(HttpClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            Client.DefaultRequestHeaders.Clear();
        }
        /// <summary>
        /// Dispose the HttpClient
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Client?.Dispose();
                }
                _disposed = true;
            }
        }
    }
}

namespace CleanArchitecture.ApiShared.Helpers.Handlers
{
    public class TimeOutDelegatingHandler(int timeOutInSecond) : DelegatingHandler
    {
        private readonly TimeSpan _timeOut = TimeSpan.FromSeconds(timeOutInSecond);
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using var linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            linkedCancellationTokenSource.CancelAfter(_timeOut);
            try
            {
                return await base.SendAsync(request, linkedCancellationTokenSource.Token);
            }
            catch (OperationCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                throw new TimeoutException("The request timed out.", ex);
            }
        }
    }
}

namespace CleanArchitecture.ApiShared.Helpers.Handlers
{
    public class RetryPolicyDelegatingHandler(int maximumAmountOfRetries) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage? response = null;
            for (var i = 0; i < maximumAmountOfRetries; i++)
            {
                response = await base.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
            }
            return response!;
        }
    }
}

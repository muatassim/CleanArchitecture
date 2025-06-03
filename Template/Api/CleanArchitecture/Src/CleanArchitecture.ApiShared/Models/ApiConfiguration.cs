namespace CleanArchitecture.ApiShared.Models
{
    public class ApiConfiguration
    {
        public ApiConfiguration()
        {
            ApiUrl = string.Empty;
            ValidateMessage = string.Empty;
            TimeoutInSeconds = 60;
            MaximumAmountOfRetries = 3;
        }
        public ApiConfiguration(string apiUrl,
            int maximumAmountOfRetries,
            int timeoutInSeconds)
        {
            ApiUrl = apiUrl;
            ValidateMessage = string.Empty;
            if (!IsValid())
            {
                throw new ArgumentException(ValidateMessage);
            }
            Init();
            MaximumAmountOfRetries = maximumAmountOfRetries;
            TimeoutInSeconds = timeoutInSeconds;
        }
        private void Init()
        {
            if (!ApiUrl.EndsWith('/'))
                ApiUrl = $"{ApiUrl}/";
        }
        public int MaximumAmountOfRetries { get; set; }
        public int TimeoutInSeconds { get; set; }
        /// <summary>
        /// EnableAuthentication 
        /// </summary>
        public bool EnableAuthentication { get; set; }
        /// <summary>
        /// Identity Server Api Url 
        /// </summary>
        public string ApiUrl { get; set; }
        public string ValidateMessage { get; private set; }
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(ApiUrl))
            {
                ValidateMessage = " API Url is required.";
                return false;
            }
            return true;
        }
    }
}

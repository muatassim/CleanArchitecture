namespace CleanArchitecture.ApiShared.Models.Options
{
    public class TokenProvider
    {
        public TokenProvider()
        {
            TokenUrl = string.Empty;
            ClientId = string.Empty;
            ClientSecret = string.Empty;
            Scope = string.Empty;
            ValidateMessage = string.Empty;
        }
        /// <summary>
        /// Shared Model 
        /// </summary>
        /// <param name="tokenUrl"></param>
        /// <param name="apiUrl"></param>
        /// <param name="clientId"></param>
        /// <param name="clientsecret"></param>
        /// <param name="scope"></param>
        public TokenProvider(string tokenUrl,
            string clientId,
            string clientsecret,
            string scope)
        {
            TokenUrl = tokenUrl;
            ClientId = clientId;
            ClientSecret = clientsecret;
            Scope = scope;
            ValidateMessage = string.Empty;
            if (!IsValid())
            {
                throw new ArgumentException(ValidateMessage);
            }
            Init();
        }
        private void Init()
        {
            if (!TokenUrl.EndsWith('/'))
                TokenUrl = $"{TokenUrl}/";
        }
        /// <summary>
        /// Identity Server Url 
        /// </summary>
        public string TokenUrl { get; set; }
        /// <summary>
        /// Client Id 
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// Client Secret 
        /// </summary>
        public string ClientSecret { get; set; }
        /// <summary>
        /// Scope 
        /// </summary>
        public string Scope { get; set; }
        public string ValidateMessage { get; private set; }
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(TokenUrl))
            {
                ValidateMessage = "Token Url is required.";
                return false;
            }
            if (string.IsNullOrEmpty(ClientId))
            {
                ValidateMessage = "Client Id is required.";
                return false;
            }
            if (string.IsNullOrEmpty(ClientSecret))
            {
                ValidateMessage = "Client Secret is required.";
                return false;
            }
            if (string.IsNullOrEmpty(Scope))
            {
                ValidateMessage = "Scope is required.";
                return false;
            }
            return true;
        }
    }
}

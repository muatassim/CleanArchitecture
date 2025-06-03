namespace CleanArchitecture.Api.Settings
{
    public class AppSettings
    {
        public string? Authority { get; set; }
        public string? Audience { get; set; }
        /// <summary>
        /// Change Email Url 
        /// </summary>
        public required string AllowedHostNames { get; set; }
        /// <summary>
        /// Allowed Hosts
        /// </summary>
        public List<string> AllowedHosts
        {
            get
            {
                List<string> hosts = [];
                if (AllowedHostNames != null)
                {
                    var urls = AllowedHostNames.Split(',');
                    foreach (var url in urls)
                    {
                        hosts.Add(url);
                    }
                }
                return hosts;
            }
        }
        /// <summary>
        /// Comma Delimited Cor Sites 
        /// </summary>
        public required string AllowedCorSites { get; set; }
        /// <summary>
        /// Allowed Hosts
        /// </summary>
        public string[] AllowedCors
        {
            get
            {
                List<string> sites = [];
                if (AllowedCorSites != null)
                {
                    var urls = AllowedCorSites.Split(',');
                    foreach (var url in urls)
                    {
                        sites.Add(url);
                    }
                }
                return [.. sites];
            }
        }
        public bool UseKeyVault { get; set; }
        public bool UseSecrets { get; set; }
        public string? AzureKeyVaultName { get; set; }
    }
}

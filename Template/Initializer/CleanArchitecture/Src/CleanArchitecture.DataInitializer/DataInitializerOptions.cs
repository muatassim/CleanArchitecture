using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
namespace  CleanArchitecture.DataInitializer
{
    public class DataInitializerOptions
    {
        public DataInitializerOptions()
        {
            ValidateMessage = string.Empty;
        }
        [JsonPropertyName("deployDacPac")]
        public bool DeployDacPac { get; set; } = true;
        [JsonPropertyName("databaseName")]
        public required string DatabaseName { get; set; }
        [JsonPropertyName("connectionString")]
        public string ConnectionString { get; set; } = string.Empty;
        [JsonPropertyName("dacPacPath")]
        public string DacPacPath { get; set; } = Path.Combine(ExecutingDirectory, "DacPac");
        [JsonPropertyName("runSeedData")]
        public bool RunSeedData { get; set; }
        [JsonPropertyName("recreateDatabase")]
        public bool RecreateDatabase { get; set; }
        // Get the executing directory from the assembly
        private static string ExecutingDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        public string DacPacFile { get; set; } = string.Empty;
        public string DacPacFilePath
        {
            get
            {
                return Path.Combine(DacPacPath, DacPacFile);
            }
        }
        public string ScriptsFolderPath
        {
            get
            {
                return Path.Combine(ExecutingDirectory, "Scripts");
            }
        }
        public string GetDatabaseConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionString)
            {
                InitialCatalog = DatabaseName,
                //add the connectiontimeout to 5 minutes
                ConnectTimeout = 600,
                //Command Timeout=600 seconds
                //Add TrustServerCertificate to true
                TrustServerCertificate = true
            };
            //Add TrustConnection to true
            builder.TrustServerCertificate = true;
            return builder.ConnectionString;
        }
        public string GetAdminConnectionString()
        {
            var builder = new SqlConnectionStringBuilder(ConnectionString);
            builder.InitialCatalog = "master";
            //add the connectiontimeout to 5 minutes
            builder.ConnectTimeout = 600;
            //Command Timeout=600 seconds
            //Add TrustServerCertificate to true
            builder.TrustServerCertificate = true;
            //Add TrustConnection to true
            builder.TrustServerCertificate = true;
            return builder.ConnectionString;
        }
        public string ValidateMessage { get; set; }
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(DatabaseName))
            {
                ValidateMessage = "DatabaseName is required";
                return false;
            }
            if (string.IsNullOrEmpty(ConnectionString))
            {
                ValidateMessage = "ConnectionString is required";
                return false;
            }
            return true;
        }
    }
    public class ConnectionStringHelper
    {
        public static  string GetDatabaseName(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString); 
            if (builder.InitialCatalog.Equals("master", StringComparison.OrdinalIgnoreCase))
                builder.InitialCatalog = "CleanArchitecture";
           return builder.InitialCatalog ?? builder.DataSource;
        }
    }

}

using System.Text;
using Microsoft.Data.SqlClient;
using CleanArchitecture.DataInitializer.Helper;
namespace CleanArchitecture.DataInitializer
{
    public class DatabaseCreator(DataInitializerOptions options)
    {
        readonly DataInitializerOptions _options = options;

        public void Run()
        {
            if (!IsUserAdmin())
            {
                throw new InvalidOperationException("The current user does not have administrative privileges on the SQL Server instance. Please use command : >>> ALTER SERVER ROLE sysadmin ADD MEMBER [Your_User_Name_Goes_Here]");
            } 
            VerifyDacPacFileExists();
            if (_options.RecreateDatabase)
            {
                DeleteDatabase();
            }
            if (!DatabaseExists())
            {
                CreateDatabase();
            }
            SeedData();
        }
        private void VerifyDacPacFileExists()
        {
            if (!File.Exists(_options.DacPacFilePath))
            {
                throw new InvalidOperationException($"DacPac file not found at {_options.DacPacFilePath}");
            }
        }
        private void CreateDatabase()
        {
            DatabaseDeploymentHelper.Instance.ProcessDacPac(
                 _options.GetAdminConnectionString(),
                 $"{_options.DatabaseName}",
                 $"{_options.DacPacFilePath}");
        }
        private void SeedData()
        {
            if (!_options.RunSeedData)
                return;
            ScriptService.RunScripts(_options.ScriptsFolderPath, _options.GetDatabaseConnectionString());
        }
        private void DeleteDatabase()
        {
            var codeBuilder = new StringBuilder();
            codeBuilder.Append($"USE [master];{Environment.NewLine}");
            codeBuilder.Append($"DECLARE @kill varchar(8000) = '';  {Environment.NewLine}");
            codeBuilder.Append($"SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'   FROM sys.dm_exec_sessions WHERE database_id  = db_id('UnitTesting'){Environment.NewLine}");
            codeBuilder.Append($"EXEC(@kill);{Environment.NewLine}");
            codeBuilder.Append($"/****** Object:  Database [{_options.DatabaseName}]    Script Date: 12/05/2018 23:06:16 ******/{Environment.NewLine}");
            codeBuilder.Append($"USE [master]{Environment.NewLine}");
            codeBuilder.Append($"IF EXISTS(SELECT * FROM SYS.DATABASES WHERE NAME = '{_options.DatabaseName}'){Environment.NewLine}");
            codeBuilder.Append($"    DROP DATABASE [{_options.DatabaseName}]{Environment.NewLine}");
            ScriptService.ExecuteScript(_options.GetAdminConnectionString(), codeBuilder);
        }
        private bool DatabaseExists()
        {
            try
            {
                var query = $"SELECT name FROM sys.databases WHERE name = '{_options.DatabaseName}'";
                using var connection = new SqlConnection(_options.GetAdminConnectionString());
                using var command = new SqlCommand(query, connection);
                connection.Open();
                var result = command.ExecuteScalar();
                return result != null;
            }
            catch
            {
                return false;
            }
        }
        private bool IsUserAdmin()
        {
            try
            {
                var query = "SELECT IS_SRVROLEMEMBER('sysadmin')";
                using var connection = new SqlConnection(_options.GetAdminConnectionString());
                using var command = new SqlCommand(query, connection);
                connection.Open();
                var result = command.ExecuteScalar();
                return result != null && (int)result == 1;
            }
            catch
            {
                return false;
            }
        }
    }
}

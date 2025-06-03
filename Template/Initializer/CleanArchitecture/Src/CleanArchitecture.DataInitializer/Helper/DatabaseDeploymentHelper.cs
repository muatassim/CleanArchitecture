using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Dac;
namespace CleanArchitecture.DataInitializer.Helper
{
    public class DatabaseDeploymentHelper
    {
        private static readonly Lazy<DatabaseDeploymentHelper> instance = new(() => new DatabaseDeploymentHelper());
        public static DatabaseDeploymentHelper Instance => instance.Value;
        public List<string> List { get; set; }
        private DatabaseDeploymentHelper() => List = [];
        public bool ProcessDacPac(string connectionString, string databaseName, string dacPacName)
        {
            var success = true;
            List.Add("*** Start of processing for " + databaseName);
            var dacOptions = new DacDeployOptions
            {
                BlockOnPossibleDataLoss = false,
                CreateNewDatabase = true
            };
            var dacServiceInstance = new DacServices(connectionString);
            dacServiceInstance.ProgressChanged += (s, e) => List.Add(e.Message);
            dacServiceInstance.Message += (s, e) => List.Add(e.Message.Message);
            try
            {
                using var dacPac = DacPackage.Load(dacPacName);
                dacServiceInstance.Deploy(dacPac,
                    databaseName,
                    upgradeExisting: true,
                    options: dacOptions);
            }
            catch (Exception ex)
            {
                success = false;
                List.Add(ex.Message);
            }
            return success;
        }
        public bool CreateBacPac(string connectionString, string databaseName, string path)
        {
            var success = true;
            List.Add("*** Start of processing for " + databaseName);
            var service = new DacServices(connectionString);
            service.ProgressChanged += (s, e) => List.Add(e.Message);
            service.Message += (s, e) => List.Add(e.Message.Message);
            try
            {
                string filePath = System.IO.Path.Combine(path, "{databaseName}.bacpac");
                service.ExportBacpac(
                   packageFileName: filePath,
                   databaseName: databaseName,
                   tables: null);
            }
            catch (Exception ex)
            {
                success = false;
                List.Add(ex.Message);
            }
            return success;
        }
        public bool CreateDacPac(string connectionString, string databaseName, string path)
        {
            var success = true;
            List.Add("*** Start of processing for " + databaseName);
            var service = new DacServices(connectionString);
            try
            {
                string filePath = System.IO.Path.Combine(path, $"{databaseName}.dacpac");
                service.Extract(
                   targetPath: filePath,
                   databaseName: databaseName,
                   applicationName: databaseName,
                   new Version(1, 0));
            }
            catch (Exception ex)
            {
                success = false;
                List.Add(ex.Message);
            }
            return success;
        }
    }
}

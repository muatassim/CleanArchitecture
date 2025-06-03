using System.Text;
using CleanArchitecture.Core.Entities;
using Microsoft.Data.SqlClient;
namespace CleanArchitecture.DataInitializer.Helper
{
    public class ScriptService
    {
        public static StringBuilder RunScripts(string scriptFolderPath, string connectionString)
        {
            StringBuilder builder = new();
            try
            {
                if (!string.IsNullOrEmpty(scriptFolderPath))
                {
                    ExecuteFile(connectionString, Path.Combine(scriptFolderPath, "Idempotency", "IdempotencyKeys.sql"), builder);
                    ExecuteFile(connectionString, Path.Combine(scriptFolderPath, "Outbox", "OutboxMessage.sql"), builder);
                    ExecuteFile(connectionString, Path.Combine(scriptFolderPath, "Search", "Search.sql"), builder);
                    //DisableDatabaseConstraints
                    ExecuteFile(connectionString, Path.Combine(scriptFolderPath, "DisableDatabaseConstraints.sql"), builder);
                    //Execute Sequentially for each file  
                    ExecuteFile(connectionString, Path.Combine(scriptFolderPath, "Dbo", $"{nameof(Categories)}.sql"), builder);
                    //EnableDatabaseConstraints
                    ExecuteFile(connectionString, Path.Combine(scriptFolderPath, "EnableDatabaseConstraints.sql"), builder);
                }
            }
            catch (Exception f)
            {
                builder.AppendLine(f.Message);
            }
            return builder;
        }
        public static void ExecuteFile(string connectionString, string filePath, StringBuilder builder, long largeFileSizeThreshold = 1 * 1024 * 1024, int commandTimeout = 600, int batchSize = 1000)
        {
            try
            {
                FileInfo file = new(filePath);
                if (file.Extension.ToLower().EndsWith("sql"))
                {
                    var sqlScript = file.OpenText().ReadToEnd();
                    if (string.IsNullOrEmpty(sqlScript)) return;
                    if (file.Length > largeFileSizeThreshold)
                    {
                        // Split the script into batches for large files
                        var sqlBatches = SplitSqlBatches(sqlScript);
                        using SqlConnection connection = new(connectionString);
                        connection.Open();
                        for (int i = 0; i < sqlBatches.Count; i += batchSize)
                        {
                            var batchGroup = sqlBatches.Skip(i).Take(batchSize);
                            foreach (var batch in batchGroup)
                            {
                                if (!string.IsNullOrWhiteSpace(batch))
                                {
                                    using SqlCommand sqlCommand = new(batch, connection)
                                    {
                                        CommandTimeout = commandTimeout
                                    };
                                    sqlCommand.ExecuteNonQuery();
                                }
                            }
                        }
                        connection.Close();
                    }
                    else
                    {
                        // Execute the entire script in one go for small files
                        using SqlCommand sqlCommand = new(sqlScript, new SqlConnection(connectionString))
                        {
                            CommandTimeout = commandTimeout
                        };
                        sqlCommand.Connection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlCommand.Connection.Close();
                    }
                }
                else
                {
                    builder.AppendLine($"{DateTime.Today.TimeOfDay}, Scripts, details: {filePath} not executed successfully");
                }
            }
            catch (Exception ex)
            {
                builder.AppendLine($"{filePath} executed problem. {ex.Message}");
            }
        }
        public static void ExecuteScript(string connectionString, StringBuilder builder, int batchSize = 1000, int commandTimeout = 600)
        {
            try
            {
                var sqlScript = builder.ToString();
                var sqlBatches = SplitSqlBatches(sqlScript);
                using SqlConnection connection = new(connectionString);
                connection.Open();
                for (int i = 0; i < sqlBatches.Count; i += batchSize)
                {
                    var batchGroup = sqlBatches.Skip(i).Take(batchSize);
                    foreach (var batch in batchGroup)
                    {
                        if (!string.IsNullOrWhiteSpace(batch))
                        {
                            using SqlCommand sqlCommand = new(batch, connection)
                            {
                                CommandTimeout = commandTimeout
                            };
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                builder.AppendLine($"executing problem. {ex.Message}");
            }
        }
        /// <summary>
        /// Get Files Path 
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="allFiles"></param>
        /// <returns></returns>
        public static List<string> GetFilesPath(string folderPath, List<string>? allFiles = null)
        {
            allFiles ??= [];
            DirectoryInfo dir = new(folderPath);
            foreach (var fileInfo in dir.GetFiles())
            {
                allFiles.Add(fileInfo.FullName);
            }
            foreach (var dirInfo in dir.GetDirectories())
            {
                var fileList = GetFilesPath(dirInfo.FullName);
                if (fileList != null && fileList.Count > 0)
                {
                    allFiles.AddRange(fileList);
                }
            }
            return allFiles;
        }
        private static List<string> SplitSqlBatches(string sqlScript)
        {
            var batches = new List<string>();
            var scriptLines = sqlScript.Split([Environment.NewLine], StringSplitOptions.None);
            var batchBuilder = new StringBuilder();
            foreach (var line in scriptLines)
            {
                if (line.Trim().Equals("GO", StringComparison.OrdinalIgnoreCase))
                {
                    if (batchBuilder.Length > 0)
                    {
                        batches.Add(batchBuilder.ToString());
                        batchBuilder.Clear();
                    }
                }
                else
                {
                    batchBuilder.AppendLine(line);
                }
            }
            if (batchBuilder.Length > 0)
            {
                batches.Add(batchBuilder.ToString());
            }
            return batches;
        }
    }
}
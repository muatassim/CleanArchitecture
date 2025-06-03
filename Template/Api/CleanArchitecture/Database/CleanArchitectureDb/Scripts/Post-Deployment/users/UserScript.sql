-- this needs to run on the database not on master  database 

IF EXISTS(SELECT * FROM sys.sysusers WHERE name = 'CleanArchitectureDbUser')
  DROP USER [CleanArchitectureDbUser] 
GO
IF NOT EXISTS (SELECT * FROM sys.sysusers WHERE name = N'CleanArchitectureDbUser')
  CREATE USER [CleanArchitectureDbUser] FROM LOGIN [CleanArchitectureDbUser] WITH DEFAULT_SCHEMA=dbo; 
GO
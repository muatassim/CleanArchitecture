CREATE USER [DataTransferUser]
	FOR LOGIN [DataTransferUser]
	WITH DEFAULT_SCHEMA = dbo
GO
GRANT CONNECT TO [DataTransferUser]

GO 
ALTER ROLE [db_datawriter] ADD MEMBER [DataTransferUser]
GO
ALTER ROLE [db_datareader] ADD MEMBER [DataTransferUser]
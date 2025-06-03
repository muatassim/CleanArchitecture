Print 'Executing Login Scripts'
IF EXISTS(SELECT * FROM sys.sql_logins WHERE name = 'CleanArchitectureDbUser')
DROP LOGIN [CleanArchitectureDbUser]

IF NOT EXISTS (SELECT * FROM sys.sql_logins WHERE name = N'CleanArchitectureDbUser')
CREATE LOGIN [CleanArchitectureDbUser] WITH PASSWORD='$(UserPassword)'
	Print 'End Executing Login Scripts'
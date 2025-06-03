Print 'Executing Procedure with Params [dbo].[CreateOrUpdateUser]'
PRINT 'Printing Variables '
PRINT 'DatabaseName: $(DatabaseName) '
PRINT 'DefaultDataPath: $(DefaultDataPath) ' 
PRINT 'UserPassword: $(UserPassword) '  

EXEC	[dbo].[CreateOrUpdateUser]
		@login ='CleanArchitectureDbUser'

Print 'Executing Procedure with Params [dbo].[CreateOrUpdateUser]'
 Print 'Deleting Procedure [dbo].[CreateOrUpdateUser]'
Drop Procedure [dbo].[CreateOrUpdateUser]
Print 'End Deleting Procedure [dbo].[CreateOrUpdateUser]'
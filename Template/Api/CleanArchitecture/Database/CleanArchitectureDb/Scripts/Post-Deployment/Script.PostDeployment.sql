/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

---- default schema scripts

---- Default database 


 :r .\dbo\Categories.sql 


 Print 'DeployToDatabase value is :'+ N'$(DeployToDatabase)'
 IF ('$(DeployToDatabase)' = 'Local')  
  BEGIN
    :r .\users\LoginScript.sql
  END
:r .\users\UserScript.sql
:r .\users\AddUserAccess.sql


IF EXISTS(SELECT * FROM sys.procedures where name='CreateOrUpdateUser')
BEGIN
 delete from [dbo].[CreateOrUpdateUser] 
END 



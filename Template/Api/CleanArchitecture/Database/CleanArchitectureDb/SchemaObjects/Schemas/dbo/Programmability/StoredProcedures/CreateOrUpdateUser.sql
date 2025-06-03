CREATE PROCEDURE [dbo].[CreateOrUpdateUser]	 
    @login nvarchar(100)
WITH EXECUTE AS CALLER
AS
BEGIN
 SET NOCOUNT ON;
 BEGIN TRY
	  BEGIN TRANSACTION; 
        Print ('Creating User '+@login)
        declare @roleSql nvarchar(max);
        declare @alterRoleSql nvarchar(max);
        declare @viewGrantSql nvarchar(max);
        declare @procedureGrantSql nvarchar(max);
        DECLARE @NewLineChar AS CHAR(3) = SPACE(13) + CHAR(10)
        set @roleSql = 'IF NOT EXISTS(SELECT * FROM sys.sysusers WHERE name = '''+@login+''')'+ @NewLineChar +  
			        ' CREATE USER ['+@login+']'+ @NewLineChar +     
                    ' ALTER USER ['+@login+'] WITH LOGIN = ['+@login+']'+ @NewLineChar +  
                    ' ALTER USER ['+@login+'] WITH DEFAULT_SCHEMA = [dbo]'+ @NewLineChar +   
                    ' ALTER USER ['+@login+'] WITH NAME = ['+@login+'] '+ @NewLineChar ;
			
        set @alterRoleSql = 
                     ' ALTER ROLE [db_datawriter] ADD MEMBER ['+@login+']'+ @NewLineChar +  
                     ' ALTER ROLE [db_datareader] ADD MEMBER ['+@login+']'+ @NewLineChar ;

        SET @procedureGrantSql=''; 
        select @procedureGrantSql+='GRANT EXECUTE ON ['+ CONVERT(varchar(255),[s].[name]) +'].['+ CONVERT(VARCHAR(255),[obj].[name])  +'] TO ['+@login+']'+ @NewLineChar 
        from
            [sys].[all_objects] as [obj]
        inner join
            [sys].[schemas] as [s] ON [obj].[schema_id] = [s].[schema_id]
        where [obj].[type] in ('P')
        AND [s].[name] NOT IN ('SYS','INFORMATION_SCHEMA') 

        SET @viewGrantSql=''; 
        select @viewGrantSql+='GRANT Select ON ['+ CONVERT(VARCHAR(255),[s].[name]) +'].['+ CONVERT(VARCHAR(255),[obj].[name])  +'] TO ['+@login+']'+ @NewLineChar 
        from
            [sys].[all_objects] as [obj]
        inner join
            [sys].[schemas] as [s] ON [obj].[schema_id] = [s].[schema_id]
        where [obj].[type] in ('V')
        AND [s].[name] NOT IN ('SYS','INFORMATION_SCHEMA') 


        --Select @roleSql; 
        --Select @alterRoleSql;
       -- Select @procedureGrantSql;
       -- Select @viewGrantSql;
        EXEC sp_executesql @roleSql 
        EXEC sp_executesql @alterRoleSql 
        EXEC sp_executesql @procedureGrantSql
        EXEC sp_executesql @viewGrantSql
        Print ('End Creating User '+@login)
 COMMIT TRANSACTION;
 END TRY
 BEGIN CATCH
	   IF XACT_STATE() <> 0
	   ROLLBACK TRANSACTION;
    IF ERROR_PROCEDURE() <> OBJECT_NAME(@@PROCID)
    DECLARE @thisErrorMessage NVARCHAR(4000);
    DECLARE @thisErrorSeverity INT;    DECLARE @thisErrorState INT;
    DECLARE @thisErrorNumber INT;
    SET @thisErrorMessage = ERROR_MESSAGE()+ RTRIM(CONVERT(CHAR,ERROR_PROCEDURE()))
    SET @thisErrorSeverity = ERROR_SEVERITY()
    SET @thisErrorState = ERROR_STATE()
    SET @thisErrorNumber= ERROR_NUMBER()
    DECLARE @thisProcedureErrorID INT; 
    RAISERROR (@thisErrorMessage,@thisErrorSeverity, @thisErrorState );
  END CATCH
 END;
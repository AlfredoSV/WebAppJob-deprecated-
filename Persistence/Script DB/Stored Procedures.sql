
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'GetJobs'
    AND ROUTINE_TYPE = N'PROCEDURE'
)

DROP PROCEDURE dbo.GetJobs
GO

CREATE OR ALTER PROCEDURE dbo.GetJobs(@page integer, @pageSize integer, @textSearch varchar(200), @count integer OUTPUT)

AS
BEGIN
    iF @page <> 1
        SET @page = @page - 1;
    ELSE
        SET @page = 0;


    IF @textSearch <> ''
    BEGIN
            SELECT @count = COUNT(*) from Job where isactive = 1 and 
            namejob like  CONCAT('%', @textSearch,'%') or descriptionjob like  CONCAT('%', @textSearch,'%');

            SELECT * from Job where isactive = 1 and 
            namejob like  CONCAT('%', @textSearch,'%') or descriptionjob like  CONCAT('%', @textSearch,'%') order by namejob
            offset @pageSize*@page rows 
            fetch next @pageSize rows only;
    END
    ELSE
    BEGIN
            SELECT @count = COUNT(*) from Job where isactive = 1;
            SELECT * from Job where isactive = 1 order by namejob
            offset @pageSize*@page rows 
            fetch next @pageSize rows only;
    END;


END;


INSERT INTO Job Values(newid(),
'Desarrollador.Net',newid(),346.3,1.0,
2,newid(),'Presencial',
NEWID(),getdate(),getdate(),1,'1,2','logo');

SELECT * FROM Job


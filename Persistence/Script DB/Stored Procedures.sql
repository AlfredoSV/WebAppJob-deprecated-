
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'GetJobs'
    AND ROUTINE_TYPE = N'PROCEDURE'
)

DROP PROCEDURE dbo.GetJobs
GO

CREATE OR ALTER PROCEDURE dbo.GetJobs(@page integer, @pageSize integer, @count integer OUTPUT)

AS
BEGIN
    iF @page <> 1
        SET @page = @page - 1;
    ELSE
        SET @page = 0;

    SELECT @count = COUNT(*) from Job where isactive = 1;

    
    SELECT * from Job where isactive = 1 order by namejob
    offset @pageSize*@page rows 
    fetch next @pageSize rows only;

END;


INSERT INTO Job Values(newid(),
'Example',newid(),346.3,1.0,
2,newid(),'descriptiom',
NEWID(),getdate(),getdate(),1,'1,2','logo');

SELECT * FROM Job


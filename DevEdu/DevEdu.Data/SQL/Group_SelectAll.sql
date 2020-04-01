DROP PROC IF EXISTS dbo.Group_SelectAll
GO

CREATE PROC dbo.Group_SelectAll 	
AS
BEGIN
	SELECT ID, StartDate, EndDate, StartTime, Duration, CourseProgramID FROM [dbo].[Group]
END

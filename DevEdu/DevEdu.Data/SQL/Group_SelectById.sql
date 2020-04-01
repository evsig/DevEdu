DROP PROC IF EXISTS Group_SelectByID
GO

CREATE PROC dbo.Group_SelectByID	
	@ID int
AS
BEGIN
	SELECT ID, StartDate, EndDate, TimeStart, Duration, CourseProgramID
	FROM [dbo].[Group]
	WHERE ID = @ID
END
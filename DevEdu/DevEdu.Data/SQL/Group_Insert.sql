DROP PROC IF EXISTS dbo.Group_Insert
GO

CREATE PROC dbo.Group_Insert	
	@StartDate date, 
	@EndDate date, 
	@TimeStart time(0), 
	@Duration int, 
	@CourseProgramID int
AS
BEGIN
	INSERT INTO [dbo].[Group] (StartDate, EndDate, TimeStart, Duration, CourseProgramID)
	VALUES (@StartDate, @EndDate, @TimeStart, @Duration, @CourseProgramID)
	SELECT SCOPE_IDENTITY()
END
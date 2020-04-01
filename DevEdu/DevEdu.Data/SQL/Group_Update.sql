DROP PROC IF EXISTS dbo.Group_Update
GO

CREATE PROC dbo.Group_Update
	@id numeric(18, 0),
	@StartDate date, 
	@EndDate date, 
	@TimeStart time(0), 
	@Duration int, 
	@CourseProgramID int
AS
BEGIN
	UPDATE [dbo].[Group]
	SET StartDate = @StartDate, 
	EndDate = @EndDate,
	TimeStart = @TimeStart,
	Duration = @Duration,
	CourseProgramID = @CourseProgramID
	WHERE ID = @ID
END
GO

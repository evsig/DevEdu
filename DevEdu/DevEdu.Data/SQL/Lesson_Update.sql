DROP PROC IF EXISTS dbo.Lesson_Update
GO

CREATE PROC dbo.Lesson_Update
	@ID numeric(18, 0),
	@GroupID int, 
	@Date date, 
	@Hometask nvarchar(200), 
	@Videos nvarchar(100), 
	@ToRead nvarchar(100)
AS
BEGIN
	UPDATE dbo.Lesson
	SET GroupID = @GroupID, 
	Date = @Date,
	Hometask = @Hometask,
	Videos = @Videos,
	ToRead = @ToRead
	WHERE ID = @ID
END
GO


DROP PROC IF EXISTS dbo.Lesson_Insert
GO

CREATE PROC dbo.Lesson_Insert	
	@GroupID int, 
	@Date date, 
	@Hometask nvarchar(200), 
	@Videos nvarchar(100), 
	@ToRead nvarchar(100)
AS
BEGIN
	INSERT INTO dbo.Lesson (GroupID, Date, Hometask, Videos, ToRead)
	VALUES (@GroupID, @Date ,@Hometask ,@Videos ,@ToRead)
	SELECT SCOPE_IDENTITY()
END
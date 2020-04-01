DROP PROC IF EXISTS dbo.Lesson_SelectByID
GO

CREATE PROC dbo.Lesson_SelectByID	
	@ID int
AS
BEGIN
	SELECT ID, GroupID, Date, Hometask, Videos, ToRead 
	FROM dbo.Lesson
	WHERE ID = @ID
END
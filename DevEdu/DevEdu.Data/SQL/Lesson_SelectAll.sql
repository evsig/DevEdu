DROP PROC IF EXISTS dbo.Lesson_SelectAll
GO

CREATE PROC dbo.Lesson_SelectAll 	
AS
BEGIN
	SELECT ID, GroupID, Date, Hometask, Videos, ToRead FROM dbo.Lesson
END
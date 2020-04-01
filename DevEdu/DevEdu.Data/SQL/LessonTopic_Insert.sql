DROP PROC IF EXISTS dbo.LessonTopic_Insert
GO

CREATE PROC dbo.LessonTopic_Insert
			@LessonID int,
			@ThemeDetailsID int
AS 
BEGIN
           INSERT INTO dbo.LessonTopic (LessonID,ThemeDetailsID)
		   VALUES (@LessonID,@ThemeDetailsID)

		   SELECT SCOPE_IDENTITY()
END
DROP PROC IF EXISTS dbo.LessonTopic_SelectAll
GO

CREATE PROC dbo.LessonTopic_SelectAll

AS 
BEGIN
          SELECT ID, LessonID,ThemeDetailsID 
		   FROM  dbo.LessonTopic

END
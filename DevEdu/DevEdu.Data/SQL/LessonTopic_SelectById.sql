DROP PROC IF EXISTS dbo.LessonTopic_SelectById
GO

CREATE PROC dbo.LessonTopic_SelectById
          @id int
AS 
BEGIN
          SELECT ID, LessonID,ThemeDetailsID 
		   FROM  dbo.LessonTopic
		   WHERE ID=@id

END
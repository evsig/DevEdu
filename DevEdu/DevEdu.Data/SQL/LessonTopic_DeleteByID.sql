DROP PROC IF EXISTS dbo.LessonTopic_DeleteByID
GO

CREATE PROC [dbo].[LessonTopic_DeleteByID]
         @ID int
		 
		 
AS
BEGIN
	DELETE LessonTopic
    FROM dbo.LessonTopic 
	WHERE ID=@ID
	
END


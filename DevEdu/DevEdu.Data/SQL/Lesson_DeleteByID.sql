DROP PROC IF EXISTS dbo.Lesson_DeleteByID
GO

CREATE PROC [dbo].[Lesson_DeleteById]
        @ID int
		 
AS
BEGIN
	DELETE 
	FROM dbo.Lesson
	WHERE ID = @ID
END


	
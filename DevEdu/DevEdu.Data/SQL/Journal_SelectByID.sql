DROP PROC IF EXISTS dbo.Journal_SelectByID
GO

CREATE PROC dbo.Journal_SelectByID	
	@ID int
AS
BEGIN
	SELECT ID, UserID, LessonID, IsAbsent, Feadback, AbsentReason 
	FROM dbo.Journal
	WHERE ID = @ID
END
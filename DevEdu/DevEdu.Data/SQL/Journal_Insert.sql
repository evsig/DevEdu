DROP PROC IF EXISTS dbo.Journal_Insert
GO

CREATE PROC dbo.Journal_Insert	
	@UserID int, 
	@LessonID int, 
	@IsAbsent bit, 
	@Feadback nvarchar(1000), 
	@AbsentReason nvarchar(100)
AS
BEGIN
	INSERT INTO dbo.Journal (UserID, LessonID ,IsAbsent ,Feadback ,AbsentReason)
	VALUES (@UserID, @LessonID ,@IsAbsent ,@Feadback ,@AbsentReason)
END
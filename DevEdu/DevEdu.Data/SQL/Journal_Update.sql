DROP PROC IF EXISTS dbo.Journal_Update
GO

CREATE PROC dbo.Journal_Update
	@ID numeric(18, 0),
	@UserID int, 
	@LessonID int, 
	@IsAbsent bit, 
	@Feadback nvarchar(1000), 
	@AbsentReason nvarchar(100)
AS
BEGIN
	UPDATE dbo.Journal
	SET UserID = @UserID, 
	LessonID = @LessonID,
	IsAbsent = @IsAbsent,
	Feadback = @Feadback,
	AbsentReason = @AbsentReason
	WHERE ID = @ID
END
GO

DROP PROC IF EXISTS dbo.User_GetBlankFields
GO

CREATE PROC dbo.User_GetBlankFields	
	@UserId int
AS
BEGIN
	SELECT j.ID JournalId, l.Date LessonDate
	FROM dbo.Journal j
		JOIN [dbo].[Lesson] l ON j.LessonID = l.ID
	WHERE (Feadback is null and IsAbsent = 0)
		or (AbsentReason is null and IsAbsent = 1)
		and GETDATE() > DATEADD(DAY, 3, l.Date)
		and j.UserId = @UserId
END
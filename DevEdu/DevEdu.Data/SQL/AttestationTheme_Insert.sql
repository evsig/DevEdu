DROP PROCEDURE IF EXISTS [dbo].[AttestationTheme_Insert]
GO

CREATE PROCEDURE [dbo].[AttestationTheme_Insert]
	@CourseId int,
	@Theme nvarchar(100)
AS
BEGIN
	INSERT INTO AttestationTheme (CourseId, Theme)
	VALUES (@CourseId, @Theme)
	SELECT SCOPE_IDENTITY()
END
GO
DROP PROCEDURE IF EXISTS [dbo].[AttestationTheme_Update]
GO

CREATE PROCEDURE [dbo].[AttestationTheme_Update]
	@Id int,
	@CourseId int,
	@Theme nvarchar(100)
AS

BEGIN
	UPDATE dbo.AttestationTheme
	Set CourseId =  @CourseId, Theme = @Theme
	WHERE ID = @id
END;
DROP PROCEDURE IF EXISTS [dbo].[AttestationTheme_SelectById]
GO

CREATE PROCEDURE [dbo].[AttestationTheme_SelectById]
	@Id int
AS
BEGIN
	SELECT ID, CourseId, Theme
	FROM dbo.AttestationTheme
	WHERE ID = @Id
END
GO
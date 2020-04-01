DROP PROCEDURE IF EXISTS [dbo].[AttestationTheme_Delete]
GO

CREATE PROCEDURE [dbo].[AttestationTheme_IDelete]
	@Id int
AS
BEGIN
	DELETE FROM dbo.AttestationTheme
	WHERE Id = @Id
END
GO
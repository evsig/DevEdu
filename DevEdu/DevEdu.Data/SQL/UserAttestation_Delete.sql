DROP PROCEDURE IF EXISTS [dbo].[UserAttestation_Delete]
GO

CREATE PROCEDURE [dbo].[UserAttestation_Delete]
	@Id int
AS
BEGIN
	DELETE FROM dbo.UserAttestation
	WHERE Id = @Id
END
GO
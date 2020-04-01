DROP PROCEDURE IF EXISTS [dbo].[UserAttestation_SelectById]
GO

CREATE PROCEDURE [dbo].[UserAttestation_SelectById]
	@Id int
AS
BEGIN
	SELECT *
	FROM dbo.UserAttestation
	WHERE Id = @Id
END
GO
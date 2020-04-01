DROP PROCEDURE IF EXISTS [dbo].[UserAttestation_SelectAll]
GO

CREATE PROCEDURE [dbo].[UserAttestation_SelectAll]
AS
BEGIN
	SELECT Id, UserId, AttestationThemeId, TheoryPassed, PracticePassed
	FROM dbo.UserAttestation
END
GO
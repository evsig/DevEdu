DROP PROCEDURE IF EXISTS [dbo].[UserAttestation_Insert]
GO

CREATE PROCEDURE [dbo].[UserAttestation_Insert]
	@UserId int,
	@AttestationThemeId int,
	@TheoryPassed bit,
	@PracticePassed bit
AS
BEGIN
	INSERT INTO UserAttestation (UserId, AttestationThemeId, TheoryPassed, PracticePassed)
	VALUES (@UserId, @AttestationThemeId, @TheoryPassed, @PracticePassed)

	SELECT SCOPE_IDENTITY()
END
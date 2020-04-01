DROP PROCEDURE IF EXISTS [dbo].[UserAttestation_Update]
GO

CREATE PROCEDURE [dbo].[UserAttestation_Update]
    @Id int,
	@UserId int,
	@AttestationThemeId int,
	@TheoryPassed bit,
	@PracticePassed bit
AS
BEGIN
	Update dbo.UserAttestation
	SET UserId = @UserId, AttestationThemeId = @AttestationThemeId, TheoryPassed = @TheoryPassed, PracticePassed = @PracticePassed
	WHERE Id = @Id
END
GO
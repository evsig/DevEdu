DROP PROC IF EXISTS dbo.[User_BioUpdate]
GO

Create PROC [dbo].[User_BioUpdate]
	@id int,
	@bio nvarchar(1000)
AS
BEGIN
	UPDATE dbo.[User]
	SET Bio = @bio
	WHERE ID = @ID

	SELECT @id
END
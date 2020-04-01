DROP PROC IF EXISTS dbo.Journal_DeleteByID
GO

CREATE PROC dbo.Journal_DeleteByID
	@ID int
AS
BEGIN
	DELETE 
	FROM dbo.Journal
	WHERE ID = @ID
END
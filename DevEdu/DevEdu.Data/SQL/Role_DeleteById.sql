DROP PROC IF EXISTS dbo.Role_DeleteById
GO

CREATE PROC [dbo].Role_DeleteById
	@id int
AS
BEGIN
	DELETE FROM dbo.Role 
	WHERE ID = @id
END

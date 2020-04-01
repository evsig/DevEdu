DROP PROC IF EXISTS dbo.[User_DeleteByID]
GO

CREATE PROC [dbo].[User_DeleteByID]
	@id int
AS
BEGIN
	DELETE 
	FROM dbo.[User]
	WHERE ID = @id
END
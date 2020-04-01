DROP PROC IF EXISTS dbo.Group_DeleteByID
GO

CREATE PROC dbo.Group_DeleteByID
	@ID int
AS
BEGIN
	DELETE 
	FROM [dbo].[Group]
	WHERE ID = @ID
END
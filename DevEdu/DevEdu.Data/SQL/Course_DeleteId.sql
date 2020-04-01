DROP PROCEDURE [dbo].[Course_DeleteId]
GO

CREATE PROC [dbo].[Course_DeleteId]
	@id int
AS
BEGIN
	DELETE FROM dbo.Course 
	WHERE ID = @id
END
GO



DROP PROCEDURE [dbo].[Course_SelectId]
GO

CREATE PROCEDURE [dbo].[Course_SelectId]
	@id int
AS
BEGIN
	SELECT ID, Name, Description, Price
	FROM dbo.Course 
	WHERE ID = @id
END
GO



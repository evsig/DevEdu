USE [DevEducation]
DROP PROCEDURE [dbo].[News_DeleteById]
GO

CREATE PROC [dbo].[News_DeleteById]
	@id int
AS
BEGIN
	DELETE FROM dbo.News
	WHERE Id = @id
END
GO


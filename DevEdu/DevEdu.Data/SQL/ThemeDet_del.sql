USE [DevEducation]

DROP PROCEDURE [dbo].[ThemeDetails_DeleteById]
GO

CREATE PROC [dbo].[ThemeDetails_DeleteById]
	@id int
AS
BEGIN
	DELETE FROM dbo.ThemeDetails
	WHERE ID = @id
END
GO


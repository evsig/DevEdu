USE [DevEducation]

DROP PROCEDURE [dbo].[ProgramDetails_SelectAll]
GO

CREATE PROC [dbo].[ProgramDetails_DeleteById]
	@id int
AS
BEGIN
	DELETE FROM dbo.ProgramDetails
	WHERE ID = @id
END
GO


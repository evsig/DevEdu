USE [DevEducation]

DROP PROCEDURE [dbo].[ThemeDetails_UpdateById]
GO

CREATE PROC [dbo].[ThemeDetails_UpdateById]
	@id int,
	@programDetailId int,
	@topic nvarchar(100)
AS
BEGIN
	UPDATE dbo.ThemeDetails
SET ProgramDetailID = @programDetailId, Topic = @topic
WHERE ID=@id
END
GO


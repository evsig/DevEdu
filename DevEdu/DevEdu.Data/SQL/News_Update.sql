USE [DevEducation]
DROP PROCEDURE [dbo].[News_UpdateById]
GO


CREATE PROC [dbo].[News_UpdateById]
	@id int,
	@title nvarchar(100),
	@content nvarchar(max),
	@authorID int,
	@recipientID int, 
	@groupID int
AS
BEGIN
	UPDATE dbo.News
SET Title = @title, 
	Content = @content,
	AuthorId = @authorID,
	RecipientID = @recipientID, 
	GroupID = @groupID
WHERE Id=@id
END
GO


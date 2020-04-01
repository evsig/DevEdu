USE [DevEducation]
DROP PROCEDURE [dbo].[News_Insert]
GO

CREATE PROCEDURE [dbo].[News_Insert] 
	@title nvarchar(100),
	@content nvarchar(max),
	@authorId int,
	@recipientID int, 
	@groupID int
AS
INSERT INTO  News (Title, Content, PublicationDate, AuthorID, RecipientID, GroupID)
VALUES (@title, @content, GETDATE(), @authorId, @recipientID, @groupID)
SELECT SCOPE_IDENTITY()


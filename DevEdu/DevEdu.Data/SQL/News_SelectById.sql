USE [DevEducation]
DROP PROCEDURE [dbo].[News_SelectById]
GO


CREATE PROCEDURE [dbo].[News_SelectById] 
	@id int
AS
Select  Id, Title, Content, PublicationDate, AuthorID, RecipientID, GroupID
From News WHERE Id = @id
GO


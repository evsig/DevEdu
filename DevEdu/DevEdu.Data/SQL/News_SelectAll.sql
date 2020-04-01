USE [DevEducation]
DROP PROCEDURE [dbo].[News_SelectAll]
GO

CREATE PROC [dbo].[News_SelectAll] AS
BEGIN
    SELECT Id, Title, Content, PublicationDate, AuthorID, RecipientID, GroupID
    FROM News
END;
GO


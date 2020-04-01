USE [DevEducation]

DROP PROCEDURE [dbo].[ThemeDetails_SelectAll]
GO

CREATE PROCEDURE [dbo].[ThemeDetails_SelectById] 
	@id int
AS
Select  ID, ProgramDetailID, Topic
From ThemeDetails WHERE ID = @id
GO


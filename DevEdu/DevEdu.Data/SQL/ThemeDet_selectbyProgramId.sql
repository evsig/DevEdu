USE [DevEducation]

DROP PROCEDURE [dbo].[ThemeDetails_SelectAll]
GO

CREATE PROCEDURE [dbo].[ThemeDetails_SelectByProgramDetailId] 
	@programDetailID int
AS
Select  ID, ProgramDetailID, Topic
From ThemeDetails WHERE ProgramDetailID = @programDetailID
GO


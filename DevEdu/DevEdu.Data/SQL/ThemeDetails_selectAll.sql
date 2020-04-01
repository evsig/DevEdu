USE [DevEducation]

DROP PROCEDURE [dbo].[ThemeDetails_SelectAll]
GO

CREATE PROC [dbo].[ThemeDetails_SelectAll] AS
BEGIN
    SELECT ID, ProgramDetailId, Topic
    FROM ThemeDetails
END;
GO


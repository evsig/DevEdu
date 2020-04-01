USE [DevEducation]

DROP PROCEDURE [dbo].[ProgramDetails_SelectAll]
GO

CREATE PROC [dbo].[ProgramDetails_SelectAll] AS
BEGIN
    SELECT ID, CourseProgramId, LessonNumber, LessonTheme
    FROM ProgramDetails
END;
GO


USE [DevEducation]

DROP PROCEDURE [dbo].[ProgramDetails_SelectById]
GO

CREATE PROCEDURE [dbo].[ProgramDetails_SelectById] 
	@id int
AS
Select  ID, CourseProgramID, LessonNumber, LessonTheme
From ProgramDetails WHERE ID = @id
GO


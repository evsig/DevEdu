USE [DevEducation]


DROP PROCEDURE [dbo].[ProgramDetails_SelectByCourseProgramId]
GO

CREATE PROCEDURE [dbo].[ProgramDetails_SelectByCourseProgramId] 
	@courseProgramID int
AS
Select  ID, CourseProgramID, LessonNumber, LessonTheme
From ProgramDetails WHERE CourseProgram = @courseProgram
GO


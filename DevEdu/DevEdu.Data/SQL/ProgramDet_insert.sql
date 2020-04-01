USE [DevEducation]

DROP PROCEDURE [dbo].[ProgramDetails_Insert]
GO

CREATE PROCEDURE [dbo].[ProgramDetails_Insert] 
	@courseProgramID int,
	@lessonNumber int,
	@lessonTheme nvarchar(100)
AS
INSERT INTO  ProgramDetails (CourseProgram, LessonNumber, LessonTheme)
VALUES (@courseProgram, @lessonNumber, @lessonTheme)
SELECT SCOPE_IDENTITY()
GO


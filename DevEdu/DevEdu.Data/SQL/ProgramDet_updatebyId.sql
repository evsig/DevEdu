USE [DevEducation]

DROP PROCEDURE [dbo].[ProgramDetails_UpdateById]
GO

CREATE PROC [dbo].[ProgramDetails_UpdateById]
	@id int,
	@courseProgramId int,
	@lessonNumber int,
	@lessontheme nvarchar(100)
AS
BEGIN
	UPDATE dbo.ProgramDetails
SET CourseProgramID = @courseProgramId, LessonNumber = @lessonNumber, LessonTheme = @lessonTheme
WHERE ID=@id
END
GO


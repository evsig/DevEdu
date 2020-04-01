USE [DevEducation]
GO
/****** Object:  StoredProcedure [dbo].[CourseProgram_SelectByCourseId]    Script Date: 26.02.2020 22:25:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CourseProgram_SelectByCourseId]
@courseId int
AS
BEGIN
	SELECT Id, CourseId, IsActual FROM dbo.CourseProgram
	WHERE CourseID = courseId;
END;
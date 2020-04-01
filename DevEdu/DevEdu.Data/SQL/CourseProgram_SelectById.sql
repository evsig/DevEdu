USE [DevEducation]
GO
/****** Object:  StoredProcedure [dbo].[CourseProgram_SelectById]    Script Date: 26.02.2020 22:25:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CourseProgram_SelectById]
@id int
AS
BEGIN

	SELECT Id, CourseId, IsActual FROM dbo.CourseProgram
	WHERE Id = id;
END;
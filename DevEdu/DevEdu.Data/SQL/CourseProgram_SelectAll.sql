USE [DevEducation]
GO
/****** Object:  StoredProcedure [dbo].[CourseProgram_SelectAll]    Script Date: 26.02.2020 22:25:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[CourseProgram_SelectAll] AS
BEGIN
    SELECT Id, CourseId, IsActual FROM dbo.CourseProgram
END;


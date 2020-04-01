USE [DevEducation]
GO
/****** Object:  StoredProcedure [dbo].[CourseProgram_Insert]    Script Date: 26.02.2020 22:24:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CourseProgram_Insert]
@courseId int,
@isActual bit
AS
BEGIN
	INSERT INTO dbo.CourseProgram
	VALUES(@courseId, @isActual)
	SELECT SCOPE_IDENTITY()
END;
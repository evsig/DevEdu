USE [DevEducation]
GO
/****** Object:  StoredProcedure [dbo].[CourseProgram_Delete]    Script Date: 26.02.2020 22:29:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CourseProgram_Delete]
@ID int
AS

BEGIN
	DELETE 
	FROM [dbo].[CourseProgram]
	WHERE ID = @ID
END
USE [DevEducation]
GO
/****** Object:  StoredProcedure [dbo].[CourseProgram_Update]    Script Date: 26.02.2020 22:21:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[CourseProgram_Update]
	@id int,
	@isActual bit
AS

BEGIN
	UPDATE dbo.CourseProgram
	Set isActual =  @isActual
	WHERE ID = @id
SELECT SCOPE_IDENTITY()
END;
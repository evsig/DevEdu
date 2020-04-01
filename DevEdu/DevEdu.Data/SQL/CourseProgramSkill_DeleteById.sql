
DROP PROCEDURE if exists [dbo].[CourseProgram_SelectById]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[CourseProgramSkill_DeleteById]
	@id int
AS
BEGIN
	DELETE FROM dbo.CourseProgramSkill  
	WHERE ID=@id
END

GO
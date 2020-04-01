DROP PROCEDURE IF EXISTS [dbo].[CourseProgramSkill_Delete]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CourseProgramSkill_Delete]
	@courseProgramid int,
	@name nvarchar(100)
AS
BEGIN
	DELETE FROM dbo.CourseProgramSkill  
	WHERE CourseProgramID=@courseProgramid and Name=@name
END

GO



DROP PROCEDURE if Exists [dbo].[CourseProgramSkill_SelectById]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CourseProgramSkill_SelectById]
@id int
AS
BEGIN

	SELECT Id, CoursePogramID, Name FROM dbo.CourseProgramSkill
	WHERE Id = @id;
END;
GO

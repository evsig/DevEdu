DROP PROC IF EXISTS [dbo].[CourseProgramSkill_SelectAll]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[CourseProgramSkill_SelectAll] AS
BEGIN
    SELECT Id, CourseProgramID, Name FROM dbo.CourseProgramSkill
END;

GO


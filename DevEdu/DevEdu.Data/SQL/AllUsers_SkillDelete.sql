USE [DevEducation]

DROP PROCEDURE [dbo].[AllUsers_SkillDelete]
GO

CREATE PROCEDURE [dbo].[AllUsers_SkillDelete]	
	@skillId int
AS
BEGIN
	DELETE FROM dbo.User_Skill  
	WHERE SkillId = @skillId 
END
GO


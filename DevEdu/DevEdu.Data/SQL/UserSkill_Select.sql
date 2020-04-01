-- =============================================
-- Author:		<Natalia,,Name>
-- Create date: <28.02.2020,,>
-- Description:	<Удаление строки,,>
-- =============================================
DROP PROCEDURE [dbo].[UserSkill_Select]
GO

CREATE PROCEDURE [dbo].[UserSkill_Select]
	@userId int,
	@skillId int
AS
BEGIN
	SELECT ID, UserID, SkillID
	FROM dbo.User_Skill 
	WHERE UserId = @userId AND SkillID=@skillId
END

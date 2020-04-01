-- =============================================
-- Author:		<Natalia,,Name>
-- Create date: <24.02.2020,,>
-- Description:	<Удаление строки,,>
-- =============================================
DROP PROCEDURE [dbo].[UserSkill_Delete]
GO

CREATE PROCEDURE [dbo].[UserSkill_Delete]
	@userID int,
	@skillId int
AS
BEGIN
	DELETE FROM dbo.User_Skill  
	WHERE UserId=@userID AND SkillId=@skillId 
END
GO

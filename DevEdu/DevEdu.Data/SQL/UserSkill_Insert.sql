-- =============================================
-- Author:		<Natalia,,Name>
-- Create date: <24.02.2020,,>
-- Description:	<Добавление новой строки,,>
-- =============================================
DROP PROCEDURE [dbo].[UserSkill_Insert]
GO

CREATE PROCEDURE [dbo].[UserSkill_Insert]
	@userID int,
	@skillID int
AS
BEGIN
	INSERT INTO dbo.User_Skill (UserID, SkillID)
    VALUES (@userID, @skillID)
  
    SELECT SCOPE_IDENTITY() 
	--Выражение SCOPE_IDENTITY() возвращает id добавленной записи, поэтому на выходе из процедуры мы получим id новой записи.
END
GO

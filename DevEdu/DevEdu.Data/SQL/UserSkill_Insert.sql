-- =============================================
-- Author:		<Natalia,,Name>
-- Create date: <24.02.2020,,>
-- Description:	<���������� ����� ������,,>
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
	--��������� SCOPE_IDENTITY() ���������� id ����������� ������, ������� �� ������ �� ��������� �� ������� id ����� ������.
END
GO

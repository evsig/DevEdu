DROP PROC IF EXISTS dbo.StudentWithSkills_ByStudentId
GO

CREATE PROC dbo.StudentWithSkills_ByStudentId
	@UserID int
AS
BEGIN
	SELECT distinct 
		u.ID, 
		u.FirstName, 
		u.LastName, 
		u.Patronymic,
		u.BirthDate,
		u.Email,
		u.Phone,
		s.ID, 
		s.[Name] Skill
	FROM dbo.[User] u
		inner join dbo.User_Skill us ON us.UserID = u.ID
		inner join dbo.CourseProgramSkill s ON s.ID = us.SkillID	
	WHERE u.ID = @UserID
END
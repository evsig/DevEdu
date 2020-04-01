DROP PROC IF EXISTS dbo.StudentWuthSkills_ByStudentId
GO

CREATE PROC dbo.StudentWuthSkills_ByStudentId
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
		u.Photo,
		s.ID, 
		s.[Name] 
	FROM dbo.[User] u
		inner join dbo.User_Skill us ON us.UserID = u.ID
		inner join dbo.CourseProgramSkill s ON s.ID = us.SkillID	
	WHERE u.ID = @UserID
END
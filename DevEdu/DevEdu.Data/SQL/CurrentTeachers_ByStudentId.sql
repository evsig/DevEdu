DROP PROC IF EXISTS dbo.CurrentTeachers_ByStudentId
GO

CREATE PROC dbo.CurrentTeachers_ByStudentId
	@UserID int
AS
BEGIN
	SELECT u.ID, u.FirstName, u.LastName
	FROM  dbo.Student_Group sg 
		inner join dbo.[Group] g on g.ID=sg.GroupID
		inner join dbo.Teacher_Group tg ON tg.GroupID = g.ID
		inner join dbo.[User] u ON u.ID = tg.UserID
	WHERE sg.UserID =  @UserID and g.EndDate > GETDATE ( ) and tg.GroupID = g.ID
END
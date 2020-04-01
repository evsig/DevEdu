DROP PROC IF EXISTS dbo.Lessons_SelectByUserID
GO

CREATE PROC dbo.Lessons_SelectByUserID
	@UserID int
AS
BEGIN
	SELECT  l.ID ,l.GroupID, l.[Date], td.ID , td.Topic, l.Hometask, l.ToRead, l.Videos, j.ID , j.UserID, j.IsAbsent, j.Feadback, j.AbsentReason
	FROM dbo.Lesson l 
		inner join dbo.LessonTopic lt ON lt.LessonId = l.Id
		inner join dbo.ThemeDetails td ON td.Id = lt.ThemeDetailsId
		inner join dbo.Journal j ON j.LessonID = l.Id
		inner join ( SELECT top 1 GroupID 
						FROM dbo.Student_Group 
						WHERE UserId = @UserID 
						ORDER BY ID DESC
			) s ON s.GroupID = l.GroupID 
	WHERE j.UserID = @UserID
END
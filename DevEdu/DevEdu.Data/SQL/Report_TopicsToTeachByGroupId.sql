CREATE PROC [dbo].[NotCompletedTopicByGroup] @groupId int AS
BEGIN
	SELECT DISTINCT td.ProgramDetailID, td.Topic 
	FROM dbo.[Group] g
	inner join dbo.CourseProgram cp on g.CourseProgramID = cp.ID
	inner join dbo.ProgramDetails pd on pd.CourseProgramID = cp.ID
	inner join dbo.ThemeDetails td on td.ProgramDetailID = pd.ID
	WHERE g.ID = @groupId AND td.ProgramDetailID not in
	(SELECT td.ProgramDetailID
	FROM dbo.Lesson l
	inner join dbo.LessonTopic lt on l.ID = lt.LessonID
	inner join dbo.ThemeDetails td on lt.ThemeDetailsID = td.ID
	WHERE l.GroupID = @groupId)
END;
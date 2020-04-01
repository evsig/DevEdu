CREATE PROC [dbo].[DetailedRating] AS
BEGIN
	SELECT c.[Name], sg.Rating, count(sg.UserID) as CountOfStudents FROM dbo.Student_Group sg
	inner join dbo.[Group] g on sg.GroupID = g.ID 
	inner join dbo.[CourseProgram] cp on g.CourseProgramID = cp.ID
	inner join dbo.Course c on cp.CourseID = c.ID
	GROUP BY sg.Rating, cp.CourseID, c.[Name]
	HAVING sg.Rating IS NOT NULL
END;
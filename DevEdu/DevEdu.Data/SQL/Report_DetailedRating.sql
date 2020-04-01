CREATE PROC [dbo].[Report_DetailedRating] @startDate Date = NULL, @endDate Date = NULL AS
BEGIN
	SELECT c.[Name] as CourseName, count(sg.UserID) as CountOfStudents, sg.Rating as RatingByCourse  FROM dbo.Student_Group sg
	inner join dbo.[Group] g on sg.GroupID = g.ID 
	inner join dbo.[CourseProgram] cp on g.CourseProgramID = cp.ID
	inner join dbo.Course c on cp.CourseID = c.ID
	WHERE (@startDate IS NOT NULL AND @endDate IS NOT NULL AND g.StartDate >= @startDate AND g.EndDate <= @endDate) 
	OR (@startDate IS NULL AND @endDate IS NULL )
	GROUP BY sg.Rating, c.[Name]
	HAVING sg.Rating IS NOT NULL 
END;
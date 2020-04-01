CREATE PROC [dbo].[Report_AverageRating] @startDate Date = NULL, @endDate Date = NULL AS
BEGIN
	SELECT c.Name as CourseName, Count(sg.UserID) as CountOfStudents, AVG(sg.Rating) as RatingByCourse
	FROM dbo.Student_Group sg
	inner join dbo.[Group] g on g.ID = sg.GroupID
	inner join dbo.[CourseProgram] cp on cp.ID = g.CourseProgramID
	inner join dbo.[Course] c on c.ID = cp.CourseID
	WHERE (@startDate IS NOT NULL AND @endDate IS NOT NULL AND g.StartDate >= @startDate AND g.EndDate <= @endDate) 
	OR (@startDate IS NULL AND @endDate IS NULL )
	GROUP BY c.Name
END;
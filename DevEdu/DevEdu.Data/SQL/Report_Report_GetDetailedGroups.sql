DROP PROC IF EXISTS dbo.Report_GetDetailedGroups
GO

CREATE PROC dbo.Report_GetDetailedGroups
AS
BEGIN
	SELECT 
		g.id,
		g.startDate,
		g.endDate,
		(
			SELECT
				COUNT(*)
			FROM dbo.Student_Group sg
			WHERE sg.GroupID = g.ID
		) as StudentCount,
		(
			SELECT
				COUNT(*)
			FROM dbo.Teacher_Group tg
			WHERE tg.GroupID = g.ID
		) TeacherCount
	FROM dbo.[Group] g
	inner join dbo.Student_Group sg
		ON g.ID = sg.GroupID
	inner join dbo.Teacher_Group tg
		ON g.ID = tg.GroupID
	WHERE g.EndDate < GETDATE()
	GROUP BY g.id, g.startDate,	g.endDate
END
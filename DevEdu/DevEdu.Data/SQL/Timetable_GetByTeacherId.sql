-- =============================================
-- Author:		<Natalia,,Name>
-- Create date: <20.03.2020,,>
-- Description:	<ѕолучить расписание преподавател€ преподавател€,,>
-- =============================================
DROP PROCEDURE [dbo].Timetable_GetByTeacherId
GO

CREATE PROCEDURE [dbo].Timetable_GetByTeacherId

	@userId int

AS

BEGIN
	select tt.ID, tt.GroupID, tt.RoomNumber, tt.[Day],tt.TimeStart,  tt.TimeEnd, c.ID, c.Name 
	from dbo.[Teacher_Group] tg  
		inner join dbo.[Group] g on tg.GroupID=g.ID
		inner join dbo.[CourseProgram] cp on g.CourseProgramID=cp.ID
		inner join dbo.[Course] c on cp.CourseID=c.ID
		inner join dbo.[TimeTable] tt on tt.GroupID=g.ID
	where tg.UserId = @userId
	order by tt.[Day]
END
--Это просто набросок, пока еще недоделал
use DevEducation
declare @courseProgramId int
insert into CourseProgram select CourseID, 'false', 'Copy' From CourseProgram as cp where cp.ID = 935
select @courseProgramId = (SELECT SCOPE_IDENTITY())

insert into ProgramDetails select @courseProgramId, pd.LessonNumber, pd.LessonTheme from ProgramDetails as pd where pd.CourseProgramID = 935

select * from ProgramDetails where ProgramDetails.CourseProgramID = @courseProgramId

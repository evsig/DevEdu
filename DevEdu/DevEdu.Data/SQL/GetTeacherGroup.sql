--1) Получить список своих активных групп
DROP PROC IF EXISTS GetTeachersGroup

go
CREATE PROCEDURE GetTeacherGroup 
@userId int
As
BEGIN
Select g.ID, g.StartDate, g.EndDate, g.TimeStart
from dbo.Teacher_Group tc
inner join dbo.[Group] g on g.Id=tc.GroupID
where UserID=@userId

END
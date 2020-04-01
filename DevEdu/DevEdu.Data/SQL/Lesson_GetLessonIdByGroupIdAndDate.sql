drop proc if exists dbo.Lesson_GetLessonIdByGroupIdAndDate
go
create proc Lesson_GetLessonIdByGroupIdAndDate @groupId int , @date date as
begin
select l.Id from dbo.Lesson l 
where l.GroupID = @groupId and l.Date = @date
end;
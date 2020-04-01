USE [DevEducation]
GO
/****** Object:  StoredProcedure [dbo].[FillIsAbsentById]    Script Date: 22.03.2020 1:17:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[FillIsAbsentById] @userId int, @lessonId int as
begin
update dbo.Journal 
set IsAbsent=1
where UserId=@userId and LessonId=@lessonId
end;

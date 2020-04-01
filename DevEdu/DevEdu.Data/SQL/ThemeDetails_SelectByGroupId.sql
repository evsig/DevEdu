
USE [DevEducation]
GO
DROP PROC IF EXISTS [dbo].[ThemeDetails_SelectByGroupId]
go
CREATE PROCEDURE [dbo].[ThemeDetails_SelectByGroupId] 
	@groupId int
AS
Begin
Select  l.GroupID ,td.ID, td.ProgramDetailID, td.Topic
From ThemeDetails td
join dbo.LessonTopic lt on lt.ThemeDetailsID= td.ID
join dbo.Lesson l on l.ID=lt.LessonID
WHERE GroupID = 615
end
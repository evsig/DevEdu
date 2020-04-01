DROP PROCEDURE IF EXISTS [dbo].[Get_GroupList]
GO

CREATE PROC [dbo].[Get_GroupList] 
@groupId int
as
BEGIN
	SELECT sg.GroupId, u.FirstName, u.LastName, [Role].[Name] as RoleName
	FROM dbo.[User] as u
	inner join dbo.Student_Group sg on sg.UserId = u.Id
	inner join dbo.User_Role on User_Role.UserId = u.Id
	inner join dbo.[Role] on User_Role.RoleId = [Role].Id	
	WHERE sg.GroupID = @groupId
	Union all
	SELECT tg.GroupId, u.FirstName, u.LastName, [Role].[Name] as RoleName
	FROM dbo.[User] as u
	inner join dbo.Teacher_Group tg on tg.UserId = u.Id
	inner join dbo.User_Role on User_Role.UserId = u.Id
	inner join dbo.[Role] on User_Role.RoleId = [Role].Id	
	WHERE tg.GroupID = @groupId
END;
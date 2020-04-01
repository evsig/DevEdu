DROP PROC IF EXISTS [dbo].[User_Role_SelectByUserID]
GO

CREATE PROCEDURE [dbo].[User_Role_SelectByUserID]	
	@UserID int
AS

BEGIN
SELECT r.Id, r.Name
FROM dbo.User_Role as ur
inner join dbo.Role r on ur.RoleID = r.Id 
Where ur.UserID = @UserID
END
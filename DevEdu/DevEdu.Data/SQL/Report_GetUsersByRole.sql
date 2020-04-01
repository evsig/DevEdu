DROP PROC IF EXISTS dbo.Report_GetUsersByRoleId
GO

CREATE PROC dbo.Report_GetUsersByRoleId	
	@ID int
AS
BEGIN
	SELECT u.*
	FROM dbo.[User] u
	 join dbo.User_Role ur ON ur.UserID = u.ID
	 join dbo.[Role] r ON r.Id = ur.RoleID
	WHERE r.Id = @ID
END
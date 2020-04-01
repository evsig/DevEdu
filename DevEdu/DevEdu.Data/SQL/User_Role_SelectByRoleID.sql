USE [DevEducation]
GO
/****** Object:  StoredProcedure [dbo].[User_Role_SelectByRoleID]    Script Date: 25.02.2020 13:18:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_Role_SelectByRoleID]	
	@RoleID int
AS
BEGIN
	SELECT ID, UserID, RoleID
	FROM dbo.User_Role
	WHERE RoleID = @RoleID
END
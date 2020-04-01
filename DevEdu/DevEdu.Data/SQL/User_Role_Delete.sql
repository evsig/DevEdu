USE [DevEducation]
GO
/****** Object:  StoredProcedure [dbo].[User_Role_Delete]    Script Date: 25.02.2020 13:15:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_Role_Delete]	
	@UserID INT,
	@RoleID INT
AS
BEGIN
	DELETE from dbo.User_Role 
	WHERE @UserID = UserID AND @RoleID = RoleID
END
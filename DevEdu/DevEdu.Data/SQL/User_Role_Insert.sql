USE [DevEducation]
GO
/****** Object:  StoredProcedure [dbo].[User_Role_Insert]    Script Date: 25.02.2020 13:16:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_Role_Insert]
	@UserID int,
	@RoleID int
AS
BEGIN
	INSERT INTO dbo.User_Role(UserID, RoleID)
    VALUES (@userID, @RoleID)
  
    SELECT SCOPE_IDENTITY() 
	
END

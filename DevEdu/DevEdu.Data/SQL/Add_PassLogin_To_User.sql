USE [DevEducation]
GO

/****** Object:  StoredProcedure [dbo].[Add_PassLogin_To_User]    Script Date: 3/26/2020 4:31:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[Add_PassLogin_To_User]
	@id int,
	@password nvarchar(100),
	@login nvarchar(100)
AS
BEGIN
	UPDATE dbo.[User] 
	SET 
	Password = @password,
	Login = @login
	WHERE ID = @id
	SELECT @id
END
GO


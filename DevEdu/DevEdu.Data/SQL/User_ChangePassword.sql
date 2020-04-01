DROP PROC IF EXISTS dbo.[User_ChangePassword]
GO

CREATE PROC [dbo].[User_ChangePassword]
	@id int,
	@password nvarchar(100),
	@login nvarchar(100)
AS
BEGIN
	UPDATE dbo.[User] 
	SET 
	Password = @password
	WHERE ID = @id
END

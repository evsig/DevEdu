ALTER PROC [dbo].[Role_Update]
	@id int,
	@Name nvarchar(100)
AS
BEGIN
	UPDATE [dbo].[Role]
	SET name = @Name
	WHERE ID = @ID
	SELECT @id
END
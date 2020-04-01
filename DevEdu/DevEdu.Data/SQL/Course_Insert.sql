DROP PROCEDURE [dbo].[Course_Insert]
GO

CREATE PROC [dbo].[Course_Insert]
	@name nvarchar(100),
	@description nvarchar(100),
	@price int

AS
BEGIN

	INSERT INTO Course (Name, Description, Price)
	VALUES (@name, @description, @price)

	SELECT SCOPE_IDENTITY()
END
GO



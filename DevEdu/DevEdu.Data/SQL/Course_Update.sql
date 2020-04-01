DROP PROCEDURE [dbo].[Course_Update]
GO

CREATE PROCEDURE [dbo].[Course_Update]
	@id int,
	@name nvarchar(100),
	@description nvarchar(100),
	@price int
AS
BEGIN
	UPDATE dbo.Course
	Set Name =  @name, Description =  @description, Price =  @price
	WHERE ID = @id
END
GO



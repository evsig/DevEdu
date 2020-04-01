
CREATE PROC [dbo].[Teacher_Group.DeleteById]
@id int
AS
BEGIN
DELETE FROM dbo.Teacher_Group
WHERE ID=@id
SELECT SCOPE_IDENTITY()
END
GO
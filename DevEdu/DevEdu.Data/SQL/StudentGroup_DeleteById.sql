
CREATE PROC [dbo].[Student_Group.DeleteById]
@id int
AS
Begin
DELETE FROM dbo.Student_Group
WHERE ID=@id
SELECT SCOPE_IDENTITY()
END
GO
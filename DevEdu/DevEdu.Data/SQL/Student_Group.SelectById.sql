CREATE PROC [dbo].[Student_Group.SelectById]
@id int
AS
BEGIN
SELECT Id, UserId,GroupId,Rating FROM dbo.Student_Group
WHERE Id=@id
END
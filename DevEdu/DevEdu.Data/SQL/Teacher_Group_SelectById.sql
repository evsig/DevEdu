CREATE PROC [dbo].[Teacher_Group.SelectById]
@id int
AS
BEGIN
SELECT Id, UserId,GroupId FROM dbo.Teacher_Group
WHERE Id=@id
END

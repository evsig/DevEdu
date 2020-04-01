
CREATE PROC [dbo].[Student_Group.Delete]
	@GroupId int,
	@UserId int
AS
Begin
	DELETE FROM dbo.Student_Group
	WHERE UserId=@UserId and GroupId=@GroupId
GO
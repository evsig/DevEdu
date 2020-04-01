
CREATE PROCEDURE [dbo].[Student_Group.Insert]
	@userId int,
    	@groupId int,
	@rating int = NULL
AS
BEGIN
    INSERT INTO Student_Group ( UserId , GroupId, Rating)
    VALUES (@userId, @groupId, @rating)
  
    SELECT SCOPE_IDENTITY()
	
END

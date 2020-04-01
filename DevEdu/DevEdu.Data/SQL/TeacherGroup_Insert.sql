
CREATE PROCEDURE [dbo].[Teacher_Group.Insert]
    @userId int,
    @groupId int
AS
BEGIN
    INSERT INTO Teacher_Group ( UserId , GroupId)
    VALUES (@userId, @groupId)
  
    SELECT SCOPE_IDENTITY()
	
END

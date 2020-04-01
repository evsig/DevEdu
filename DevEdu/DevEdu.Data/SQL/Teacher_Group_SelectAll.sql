CREATE PROCEDURE [dbo].[Teacher_Group.SelectAll]
    
AS
BEGIN
    SELECT Id, UserId,GroupId FROM dbo.Teacher_Group
  
END

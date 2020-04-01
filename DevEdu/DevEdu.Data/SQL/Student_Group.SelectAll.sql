
CREATE PROCEDURE [dbo].[Student_Group.SelectAll]
    
AS
BEGIN
    SELECT Id, UserId,GroupId,Rating FROM dbo.Student_Group
  
END

DROP PROC IF EXISTS dbo.Student_Group_SelectByUserID
GO

CREATE PROC dbo.Student_Group_SelectByUserID
	@UserID int
AS
BEGIN
	SELECT ID, UserID, GroupID, Rating
	FROM dbo.Student_Group 
	WHERE UserID =  @UserID
END
CREATE PROC [dbo].[FillsRating] @userId int, @grouId int, @rating int AS
BEGIN
UPDATE dbo.Student_Group
SET Rating=@rating
WHERE UserId=@userId and GroupID=@grouId
END;
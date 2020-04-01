DROP PROC IF EXISTS dbo.StudentRating_UpdateByUserId
GO

CREATE PROC dbo.StudentRating_UpdateByUserId
	@id int,
	@rating int
AS
BEGIN
	UPDATE dbo.Student_Group
	SET Rating = @rating
	WHERE ID = @ID
END
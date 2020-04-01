DROP PROCEDURE [dbo].[Course_SelectAll]
GO

CREATE PROCEDURE [dbo].[Course_SelectAll]
AS
BEGIN
	SELECT ID, Name, Description, Price
	FROM dbo.Course
END
GO



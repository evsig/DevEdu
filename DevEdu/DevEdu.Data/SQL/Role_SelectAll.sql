DROP PROC IF EXISTS dbo.Role_SelectAll
GO

CREATE PROC [dbo].Role_SelectAll
AS
BEGIN
	SELECT ID, Name FROM dbo.Role
END

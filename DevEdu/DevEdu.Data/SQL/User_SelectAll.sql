DROP PROC IF EXISTS dbo.[User_SelectAll]
GO

CREATE PROC [dbo].[User_SelectAll]
AS
BEGIN
	SELECT ID, FirstName, LastName, Patronymic, BirthDate, Email, Phone, Password, Login, Bio, CityID, RegistrationDate FROM dbo.[User]
END

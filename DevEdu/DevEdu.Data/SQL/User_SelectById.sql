DROP PROC IF EXISTS dbo.[User_SelectById]
GO

CREATE PROC [dbo].[User_SelectById]
	@id int
AS 
BEGIN
	SELECT ID, FirstName, LastName, Patronymic, BirthDate, Email, Phone, Password, Login, Bio, CityID, RegistrationDate 
	FROM dbo.[User] 
	WHERE Id = @id
END

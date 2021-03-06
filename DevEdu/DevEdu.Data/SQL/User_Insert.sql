DROP PROC IF EXISTS [dbo].[User_Insert]
GO

CREATE PROC [dbo].[User_Insert]
	@firstName nvarchar(100),
	@lastName nvarchar(100),
	@patronymic nvarchar(100) = NULL,
	@birthDate date,
	@email nvarchar(100),
	@phone nvarchar(30),
	@password nvarchar(100) = NULL,
	@login nvarchar(100) = NULL,
	@bio nvarchar(1000) = NULL,
	@cityId int
AS
BEGIN
	INSERT INTO dbo.[User] (FirstName, LastName, Patronymic, BirthDate, Email, Phone, Password, Login, Bio, CityID, RegistrationDate)
	VALUES (@firstName, @lastName, @patronymic, @birthDate, @email, @phone, @password, @login, @bio, @cityId, GETDATE())

	SELECT SCOPE_IDENTITY()
END

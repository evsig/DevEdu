DROP PROC IF EXISTS dbo.[User_Update]
GO

CREATE PROC [dbo].[User_Update]
	@id int,
	@firstName nvarchar(100),
	@lastName nvarchar(100),
	@patronymic nvarchar(100),
	@birthDate date,
	@email nvarchar(100),
	@phone nvarchar(30),
	@password nvarchar(100),
	@login nvarchar(100),
	@bio nvarchar(1000),
	@cityId int
AS
BEGIN
	UPDATE dbo.[User]
	SET FirstName = @firstName, 
	LastName = @lastName,
	Patronymic = @patronymic,
	BirthDate = @birthDate,
	Email = @email,
	Phone = @phone,
	Password = @password,
	Login = @login,
	Bio = @bio,
	CityID = @cityId
	WHERE ID = @ID
END

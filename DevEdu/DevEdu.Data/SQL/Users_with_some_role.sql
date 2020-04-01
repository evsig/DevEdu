USE [DevEducation]
GO

/****** Object:  StoredProcedure [dbo].[Users_with_some_role]    Script Date: 3/26/2020 2:06:39 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Users_with_some_role]
	@Role_name nvarchar(100)	
	as
BEGIN
	SELECT u.ID, u.FirstName, u.LastName, u.Patronymic, u.BirthDate, u.Email, u.Phone, u.Bio, c.Name as City, u.RegistrationDate, r.Name from dbo.[User] u
	inner join dbo.[User_Role] ur on u.ID = ur.UserID
	inner join dbo.[Role] r on ur.RoleID = r.Id
	inner join dbo.[City] c on c.ID = u.CityID
	where r.Name = @Role_name
END
GO


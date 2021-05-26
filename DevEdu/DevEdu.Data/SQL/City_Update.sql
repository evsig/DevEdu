USE [DevEducation]
GO
/****** Object:  StoredProcedure [dbo].[City_Update]    Script Date: 04.03.2020 12:02:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[City_Update]
@id int,
@name nvarchar(100)
as
begin
	update dbo.City
	set Name = @name
	where id = @id
	SELECT SCOPE_IDENTITY()
end
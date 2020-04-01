create proc dbo.City_SelectByName
	@name nvarchar(100)
as
begin
select Id, Name from dbo.City where Name = @name
end
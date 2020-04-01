create proc dbo.City_SelectById
	@id int 
as
begin
    select Id, Name from dbo.City where Id = @id
end

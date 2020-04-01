DROP PROC IF EXISTS dbo.News_SelectAllForStudent
GO

CREATE PROC dbo.News_SelectAllForStudent 	
	@studentId int
	
AS
BEGIN
	SELECT n.Id, n.Title, n.Content, n.PublicationDate, u.FirstName, u.LastName
    FROM dbo.News n
		inner join dbo.[User] u ON u.ID = n.AuthorID
		left join dbo.[User] ur ON ur.ID = @studentId
		left join dbo.Student_Group sg ON sg.UserID = ur.ID
	WHERE n.PublicationDate >=  ur.RegistrationDate and( n.RecipientID = @studentId or n.GroupID=sg.GroupID or (n.RecipientID is null and n.GroupID is null ))
	ORDER BY n.PublicationDate
END


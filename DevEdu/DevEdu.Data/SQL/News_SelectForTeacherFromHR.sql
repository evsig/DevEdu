
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[News_SelectForTeacherFromHR] 	
	@teacherId int
AS
BEGIN

SELECT n.Id, n.Title, n.Content, n.PublicationDate, u.FirstName, u.LastName
    FROM dbo.News n
		inner join dbo.[User] u ON u.ID = n.AuthorID
		left join dbo.[User] rec ON rec.ID = @teacherId --1257
	WHERE n.PublicationDate >= rec.RegistrationDate --and n.Author = 252
	and (n.RecipientID = @teacherId --1257
	or (n.RecipientID is null and n.GroupID is null ))
	ORDER BY n.PublicationDate
 end
GO

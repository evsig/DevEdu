-- =============================================
-- Author:		<Natalia,,Name>
-- Create date: <06.03.2020,,>
-- Description:	<Получить список студенотов с рейтингом , учащихтся в конкретной группе у конкретного преподавателя,,>
-- =============================================
DROP PROCEDURE [dbo].[GetStudentsInGroup]
GO

CREATE PROCEDURE [dbo].[GetStudentsInGroup]
	@groupId int
AS
BEGIN
	SELECT 
	u.[id], u.[FirstName], u.[LastName], u.[Patronymic], u.[BirthDate], 
	u.[Email], u.[Phone],u.[Bio], u.[RegistrationDate], u.[Photo], c.[Id], c.[Name],sg.[Id], sg.[Rating] 
	FROM dbo.[User] u 
	
		inner join dbo.[City] c on u.CityId=c.Id
	
		inner join dbo.[Student_Group] sg on sg.UserId=u.ID
	
	WHERE u.id in
	
	(select UserID from dbo.[Student_Group]
	WHERE groupid = @groupId)

END
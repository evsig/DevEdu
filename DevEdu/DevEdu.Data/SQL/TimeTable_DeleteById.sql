-- =============================================
-- Author:		<Natalia,,Name>
-- Create date: <20.03.2020,,>
-- Description:	<Удалить расписание (одну строчку в таблице) по id,,>
-- =============================================
DROP PROCEDURE [dbo].[TimeTable_DeleteById]
GO

CREATE PROCEDURE [dbo].[TimeTable_DeleteById]
	@id int
AS
BEGIN
	DELETE 
	FROM dbo.[TimeTable]
	WHERE ID = @id

END
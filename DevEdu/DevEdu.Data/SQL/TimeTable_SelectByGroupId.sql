-- =============================================
-- Author:		<Natalia,,Name>
-- Create date: <20.03.2020,,>
-- Description:	<Выборка строки с расписание по groupId ,,>
-- =============================================
DROP PROCEDURE [dbo].[TimeTable_SelectByGroupId]
GO

CREATE PROCEDURE [dbo].[TimeTable_SelectByGroupId]
	@groupId int
AS
BEGIN
	SELECT [ID], [GroupId], [RoomNumber], [Day], [TimeStart], [TimeEnd]
	FROM dbo.[TimeTable]
	WHERE groupId = @groupId
END
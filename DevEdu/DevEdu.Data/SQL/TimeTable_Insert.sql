-- =============================================
-- Author:		<Natalia,,Name>
-- Create date: <20.03.2020,,>
-- Description:	<Добавить строку с расписанием,,>
-- =============================================
DROP PROCEDURE [dbo].[TimeTable_Insert]
GO

CREATE PROCEDURE [dbo].[TimeTable_Insert]
	@groupId int,
	@roomNumber int,
	@day date,
	@timeStart time,
	@timeEnd time
AS
BEGIN
	INSERT INTO dbo.[TimeTable]
	([GroupId], [RoomNumber], [Day], [TimeStart], [TimeEnd])
	VALUES 
	(@groupId, @roomNumber, @day, @timeStart, @timeEnd)

	SELECT SCOPE_IDENTITY()

END
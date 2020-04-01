-- =============================================
-- Author:		<Natalia,,Name>
-- Create date: <20.03.2020,,>
-- Description:	<ќЅновление строки с расписанием,,>
-- =============================================
DROP PROCEDURE [dbo].[TimeTable_Update]
GO

CREATE PROCEDURE [dbo].[TimeTable_Update]
	@id int,
	@groupId int,
	@roomNumber int,
	@day date,
	@timeStart time,
	@timeEnd time
AS
BEGIN
	UPDATE dbo.[TimeTable]
	SET 
	[GroupId] = @groupId, 
	[RoomNumber] = @roomNumber,
	[Day] = @day,
	[TimeStart] = @timeStart,
	[TimeEnd] = @timeEnd
	WHERE [ID] = @id
END
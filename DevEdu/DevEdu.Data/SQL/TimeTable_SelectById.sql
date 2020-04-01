-- =============================================
-- Author:		<Natalia,,Name>
-- Create date: <20.03.2020,,>
-- Description:	<������� ������ � ���������� �� Id ������,,>
-- =============================================
DROP PROCEDURE [dbo].[TimeTable_SelectById]
GO

CREATE PROCEDURE [dbo].[TimeTable_SelectById]
	@id int
AS
BEGIN
	SELECT [ID], [GroupId], [RoomNumber], [Day], [TimeStart], [TimeEnd]
	FROM dbo.[TimeTable]
	WHERE Id = @id
END
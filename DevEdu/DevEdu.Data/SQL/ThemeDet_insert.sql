USE [DevEducation]

DROP PROCEDURE [dbo].[ThemeDetails_Insert]
GO

CREATE PROCEDURE [dbo].[ThemeDetails_Insert] 
	@programDetailID int,
	@topic nvarchar(100)
AS
INSERT INTO  ThemeDetails (Topic, ProgramDetailID)
VALUES (@topic, @programDetailID )
SELECT SCOPE_IDENTITY()
GO


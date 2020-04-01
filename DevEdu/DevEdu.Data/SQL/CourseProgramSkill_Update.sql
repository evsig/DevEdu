DROP PROCEDURE IF EXISTS [dbo].[CourseProgramSkill_Update]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[CourseProgramSkill_Update]
	@id int,
	@courseProgramID int,
	@name nvarchar(100)
AS
BEGIN
	UPDATE dbo.CourseProgramSkill
	SET @courseProgramID=@courseProgramID, Name=@name
    WHERE ID=@id
END

GO
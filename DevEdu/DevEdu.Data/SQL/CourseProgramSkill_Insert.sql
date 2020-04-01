
DROP PROCEDURE IF EXISTS [dbo].[CourseProgramSkill_Insert]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[CourseProgramSkill_Insert]
	@courseProgramID int,
	@name nvarchar(100)
AS
BEGIN
	INSERT INTO dbo.CourseProgramSkill (CourseProgramID, Name)
    VALUES (@courseProgramID, @name)
  
    SELECT SCOPE_IDENTITY() 
END
GO
USE [DevEducation]
GO

/****** Object:  StoredProcedure [dbo].[Report_QuantityOfLessonsByTopic]    Script Date: 3/24/2020 2:35:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_QuantityOfLessonsByTopic]
	@ThemeDetailsID int	
	as
	BEGIN
	SELECT g.ID as GroupId, g.StartDate, g.EndDate, td.Topic, count(g.ID) as Quantity_of_lesson from dbo.[Lesson] l
	
	inner join dbo.[LessonTopic] lt on lt.LessonID = l.ID
	inner join dbo.[Group] g on l.GroupID = g.ID
	inner join dbo.[ThemeDetails] td on td.Id = lt.ThemeDetailsId
	where lt.ThemeDetailsID = @ThemeDetailsID
	group by g.ID, g.StartDate, g.EndDate, td.Topic
	END
GO


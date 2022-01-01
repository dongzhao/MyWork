-- =============================================
-- Author:		DongZ
-- Create date: 31/12/2021
-- Description:	
-- =============================================
DROP PROCEDURE usp_SearchUserProfile 
GO

CREATE PROCEDURE usp_SearchUserProfile 
	@dateFrom varchar(10), 
	@dateTo varchar(10)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	p.FirstName, 
			p.LastName,
			p.Gender,
			p.BirthDate,
			p.Mobile,
			p.Address,
			DATEPART(year, p.BirthDate) as Year,
			DATEPART(quarter, p.BirthDate) as Quarter,
			DATEPART(month, p.BirthDate) as Month,
			DATEPART(week, p.BirthDate) as Week,
			DATEPART(day, p.BirthDate) as Day
	FROM	[dbo].[my_profile] p
	WHERE	p.BirthDate BETWEEN CAST(@dateFrom AS DATETIME) AND CAST(@dateTo AS DATETIME)
END
GO

-- exec usp_SearchUserProfile '1900-01-01', '9999-12-31'

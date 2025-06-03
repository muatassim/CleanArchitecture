-- =============================================
-- Author: CodeBuilder
-- Create date: Wednesday, May 14, 2025
-- Description: Categories
-- =============================================
SET IDENTITY_INSERT [dbo].[Categories] ON; 
BEGIN TRANSACTION
PRINT 'Adding Records for dbo Categories - total records: 8'
-- =========================Begin Record: 1===================
IF NOT EXISTS (SELECT * FROM [dbo].[Categories] Where [CategoryID] = 101)
BEGIN
 INSERT INTO [dbo].[Categories]
(
	[CategoryID],
	[CategoryName],
	[Description] 
) VALUES 
(
 	101,
	'Beverages',
	'Soft drinks, coffees, teas, beers, and ales'
	)
END 
-- =========================End Record: 8===================
COMMIT TRANSACTION
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF; 
PRINT 'End Adding Records for dbo Categories >> 8'

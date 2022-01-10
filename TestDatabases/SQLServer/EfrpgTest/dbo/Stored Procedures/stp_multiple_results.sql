CREATE PROCEDURE [dbo].[stp_multiple_results]
AS
	SELECT  [codeObjectNo],[applicationNo] FROM [dbo].[CodeObject];
	SELECT * FROM CompanyGroup;
	SELECT * FROM Colour;

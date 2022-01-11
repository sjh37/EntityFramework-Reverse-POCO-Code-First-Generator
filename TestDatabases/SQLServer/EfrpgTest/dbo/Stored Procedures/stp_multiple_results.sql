CREATE PROCEDURE [dbo].[stp_multiple_results]
AS
BEGIN
	SELECT  [codeObjectNo],[applicationNo] FROM [dbo].[CodeObject];
	SELECT Id,
           PrimaryColourId,
           CarMake FROM Car;

	SELECT Id,Name FROM Colour;
END;

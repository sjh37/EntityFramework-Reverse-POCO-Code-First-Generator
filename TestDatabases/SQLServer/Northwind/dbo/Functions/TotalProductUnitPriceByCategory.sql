


CREATE FUNCTION [dbo].[TotalProductUnitPriceByCategory]
(@categoryID int)
RETURNS Money
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar Money

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar = (Select SUM(UnitPrice) 
						from Products 
						where CategoryID = @categoryID) 

	-- Return the result of the function
	RETURN @ResultVar

END



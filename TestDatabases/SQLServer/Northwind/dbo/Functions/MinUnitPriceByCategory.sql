
CREATE FUNCTION [dbo].[MinUnitPriceByCategory]
(@categoryID INT
)
RETURNS Money
AS
BEGIN
	-- Declare the return variable here
	DECLARE @ResultVar Money

	-- Add the T-SQL statements to compute the return value here
	SELECT @ResultVar = MIN(p.UnitPrice) FROM Products as p WHERE p.CategoryID = @categoryID

	-- Return the result of the function
	RETURN @ResultVar

END

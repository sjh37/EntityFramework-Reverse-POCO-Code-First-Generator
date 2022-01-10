


CREATE FUNCTION [dbo].[ProductsUnderThisUnitPrice]
(@price Money
)
RETURNS TABLE
AS
RETURN
	SELECT *
	FROM Products as P
	Where p.UnitPrice < @price




CREATE FUNCTION udfNetSale (@quantity INT, @list_price DECIMAL(10, 2), @discount DECIMAL(4, 2))
RETURNS DECIMAL(10, 2)
AS
BEGIN
    RETURN @quantity * @list_price * (1 - @discount);
END;

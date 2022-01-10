CREATE PROCEDURE AddTwoValues(@a INT, @b INT)
AS
BEGIN
	SET NOCOUNT ON;
	RETURN @a + @b -- 0 indicates success, anything else is a failure
END

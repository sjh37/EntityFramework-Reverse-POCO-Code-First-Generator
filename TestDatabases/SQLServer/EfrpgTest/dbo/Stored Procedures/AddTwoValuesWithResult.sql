CREATE PROCEDURE AddTwoValuesWithResult(@a INT, @b INT, @result INT OUTPUT, @result2 INT OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @result = @a + @b
	SET @result2 = @b - @a
END

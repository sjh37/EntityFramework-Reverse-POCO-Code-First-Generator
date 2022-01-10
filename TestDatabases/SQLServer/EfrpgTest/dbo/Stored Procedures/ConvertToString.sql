CREATE PROCEDURE ConvertToString(@someValue INT, @someString VARCHAR(20) OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @someString = '*' + CAST(@someValue AS VARCHAR(20)) + '*'
END

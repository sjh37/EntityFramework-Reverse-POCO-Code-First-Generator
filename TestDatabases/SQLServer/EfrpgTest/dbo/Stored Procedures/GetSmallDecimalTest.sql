CREATE PROCEDURE GetSmallDecimalTest(@maxId INT)
AS
BEGIN
	SET NOCOUNT ON;
	IF(@maxId IS NULL)
		SET @maxId = 999
	SELECT id, KoeffVed FROM SmallDecimalTest WHERE id <= @maxId
END

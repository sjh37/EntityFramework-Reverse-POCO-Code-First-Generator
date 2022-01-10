CREATE PROCEDURE [dbo].[182_test2]
	@Flag INT
AS
BEGIN
	SET NOCOUNT ON;

	IF @Flag = 1
	BEGIN
		SELECT name, type, type_desc
		FROM sys.objects
	END

	SELECT name, ISNULL(principal_id, 0) AS principal_id
	FROM sys.objects

	SELECT name, object_id
	FROM sys.objects
END

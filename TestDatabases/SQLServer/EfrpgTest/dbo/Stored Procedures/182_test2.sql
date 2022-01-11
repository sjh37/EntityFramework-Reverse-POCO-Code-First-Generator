CREATE PROCEDURE [dbo].[182_test2]
	@Flag INT
AS
BEGIN
	SET NOCOUNT ON;

	IF @Flag = 1
	BEGIN
		SELECT Id, [Description] AS DescriptionFlag1
		FROM NoPrimaryKeys
	END

	SELECT Id, ISNULL([Description], '') AS [DescriptionNotNull]
	FROM NoPrimaryKeys

	SELECT Id, [Description]
	FROM NoPrimaryKeys
END
GO
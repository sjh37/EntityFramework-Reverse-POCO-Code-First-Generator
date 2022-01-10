
CREATE FUNCTION [CustomSchema].[CsvToIntWithSchema]
(
	@array varchar(8000),
	@array2 varchar(8000) = ''
)
RETURNS @IntTable TABLE(IntValue int)
AS
BEGIN
	DECLARE @seperator char(1)
	SET @seperator = ','

	DECLARE @seperator_position int
	DECLARE @array_value varchar(8000)

	SET @array = @array + @seperator

	WHILE PATINDEX('%,%', @array) <> 0
	BEGIN
		SELECT @seperator_position = PATINDEX('%,%', @array)
		SELECT @array_value = LEFT(@array, @seperator_position - 1)

		INSERT @IntTable VALUES (CAST(@array_value AS int))

		SELECT @array = STUFF(@array, 1, @seperator_position, '')
		IF LEN(@array) = 0 AND LEN(@array2) > 0
		BEGIN
			SET @array = @array2 + ','
			SET @array2 = ''
		END
	END

	RETURN
END

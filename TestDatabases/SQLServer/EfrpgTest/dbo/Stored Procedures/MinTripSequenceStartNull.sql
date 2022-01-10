CREATE PROCEDURE MinTripSequenceStartNull(@minTripSequenceStartParam DATETIME2 OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @minTripSequenceStartParam = NULL
END

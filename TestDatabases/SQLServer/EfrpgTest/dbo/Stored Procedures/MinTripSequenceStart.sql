CREATE PROCEDURE MinTripSequenceStart(@minTripSequenceStartParam DATETIME2 OUTPUT)
AS
BEGIN
	SET NOCOUNT ON;
	SET @minTripSequenceStartParam = GETDATE()
END

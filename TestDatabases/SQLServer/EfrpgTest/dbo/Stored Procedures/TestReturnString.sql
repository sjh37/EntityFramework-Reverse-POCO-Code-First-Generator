



-- From Raju_L
-- DROP PROC TestReturnString
CREATE PROCEDURE [dbo].[TestReturnString]
AS
BEGIN
    SET NOCOUNT ON
    SET XACT_ABORT ON
    DECLARE @error VARCHAR(100)
    SET @error = ''
    IF(1+1 = 2) BEGIN
        SET @error = 'test'
    END
    SELECT @error AS error
END

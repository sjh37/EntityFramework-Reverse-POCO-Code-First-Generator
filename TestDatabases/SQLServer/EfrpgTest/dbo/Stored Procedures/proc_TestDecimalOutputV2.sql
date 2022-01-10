CREATE PROCEDURE [dbo].[proc_TestDecimalOutputV2]
    @PerfectNumber decimal(12,8) OUTPUT
AS
BEGIN
    SET @PerfectNumber = 10.35;
END

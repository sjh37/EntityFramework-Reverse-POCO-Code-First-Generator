CREATE PROCEDURE [dbo].[proc_TestDecimalOutputV3Default]
    @PerfectNumber decimal OUTPUT
AS
BEGIN
    SET @PerfectNumber = 10.35;
END

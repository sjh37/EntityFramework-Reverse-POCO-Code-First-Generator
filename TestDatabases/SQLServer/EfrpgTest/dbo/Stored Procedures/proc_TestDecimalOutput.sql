
-- From 0v3rCl0ck
CREATE PROCEDURE [dbo].[proc_TestDecimalOutput]
    @PerfectNumber decimal(18,2) OUTPUT
AS
BEGIN
    SET @PerfectNumber = 10.35;
END

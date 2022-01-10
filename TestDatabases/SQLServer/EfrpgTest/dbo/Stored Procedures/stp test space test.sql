
CREATE PROCEDURE [dbo].[stp test space test] (@a_val INT, @b_val INT)
AS
SELECT  [codeObjectNo] AS [code object no],
        [applicationNo] AS [application no]
FROM    [dbo].[CodeObject];

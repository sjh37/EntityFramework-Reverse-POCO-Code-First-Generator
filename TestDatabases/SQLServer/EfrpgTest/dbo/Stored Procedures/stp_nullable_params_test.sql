
CREATE PROCEDURE [dbo].[stp_nullable_params_test] (@a_val INT, @b_val int NULL)
AS
SELECT  [codeObjectNo],[applicationNo]
FROM    [dbo].[CodeObject];

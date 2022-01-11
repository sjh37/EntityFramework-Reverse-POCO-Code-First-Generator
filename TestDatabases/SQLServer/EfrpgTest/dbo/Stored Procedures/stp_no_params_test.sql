CREATE PROCEDURE [dbo].[stp_no_params_test]
AS
BEGIN
	SELECT  [codeObjectNo],[applicationNo]
	FROM    [dbo].[CodeObject];
END


-- #183 Return Model for functions returning nullable
CREATE FUNCTION [dbo].[182_test1]
(
	@test INT
)
RETURNS TABLE AS RETURN
(
	SELECT name, ISNULL(principal_id, 0) AS principal_id
	FROM sys.objects
)

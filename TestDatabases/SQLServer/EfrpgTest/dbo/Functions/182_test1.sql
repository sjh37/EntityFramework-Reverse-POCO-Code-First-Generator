
-- #183 Return Model for functions returning nullable
CREATE FUNCTION [dbo].[182_test1]
(
	@test INT
)
RETURNS TABLE AS RETURN
(
	SELECT Id, ISNULL([Description], '') AS [Description]
	FROM NoPrimaryKeys
)

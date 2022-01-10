CREATE FUNCTION [dbo].[fnWithCustomTableType] (@cusType dbo.CustomTableType READONLY)
RETURNS @result TABLE ([Something] NVARCHAR(64))
AS
BEGIN
    INSERT INTO @result
    SELECT 'Something'
    RETURN;
END;

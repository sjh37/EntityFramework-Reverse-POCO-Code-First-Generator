
CREATE VIEW [dbo].[view with space]
AS
SELECT  codeObjectNo,
        applicationNo,
        type,
        eName,
        aName,
        description,
        codeName,
        note,
        isObject,
        versionNumber
FROM    [dbo].[CodeObject];

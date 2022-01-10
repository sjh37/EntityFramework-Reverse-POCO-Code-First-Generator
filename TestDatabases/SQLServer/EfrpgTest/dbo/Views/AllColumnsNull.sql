CREATE VIEW AllColumnsNull
AS
    SELECT  SUM(applicationNo) AS total,
            aName --ISNULL(aName, '') AS aName
    FROM    CodeObject
    GROUP BY aName

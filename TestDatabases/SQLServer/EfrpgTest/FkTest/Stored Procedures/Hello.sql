-- DROP PROC FkTest.Hello;
CREATE PROC FkTest.Hello AS
    SELECT  [static],[readonly] -- #279 Contains static and readonly, which are illegal in C#
    FROM    ColumnName

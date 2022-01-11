-- #329 Async SPROCs with output parameters. Thanks to ymerej.
CREATE PROCEDURE dbo.InsertRecord
    @Data VARCHAR(256),
    @InsertedId INT OUT
AS
BEGIN
    INSERT INTO TableA (TableADesc) VALUES (@Data)

    SET @InsertedId = SCOPE_IDENTITY();
END;

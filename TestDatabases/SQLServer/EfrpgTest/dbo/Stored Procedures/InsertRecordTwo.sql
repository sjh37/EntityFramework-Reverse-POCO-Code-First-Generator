-- drop PROCEDURE dbo.InsertRecordTwo
CREATE PROCEDURE dbo.InsertRecordTwo
    @Data VARCHAR(256),
    @InsertedId INT OUT,
    @AnotherInsertedId INT OUT
AS
BEGIN
    INSERT INTO TableA (TableADesc) VALUES (@Data)
    SET @InsertedId = SCOPE_IDENTITY();
    SET @AnotherInsertedId = @InsertedId + 1;
END;

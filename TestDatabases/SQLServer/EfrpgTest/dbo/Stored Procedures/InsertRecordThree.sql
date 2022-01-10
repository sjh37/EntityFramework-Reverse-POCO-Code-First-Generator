
CREATE PROCEDURE dbo.InsertRecordThree
    @Data VARCHAR(256),
    @InsertedId INT OUT,
	@SomeId INT,
    @AnotherInsertedId INT OUT
AS
BEGIN
    INSERT INTO DataTable VALUES (@Data)
    SET @InsertedId = SCOPE_IDENTITY();
    SET @AnotherInsertedId = @InsertedId + 1;
END;

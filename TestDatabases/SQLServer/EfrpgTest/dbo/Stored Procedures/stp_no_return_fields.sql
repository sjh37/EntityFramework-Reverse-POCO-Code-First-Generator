CREATE PROCEDURE [dbo].[stp_no_return_fields]
AS
    UPDATE [dbo].[CodeObject] SET type=4 WHERE codeObjectNo < 1000

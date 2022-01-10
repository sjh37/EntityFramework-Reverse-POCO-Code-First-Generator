
CREATE PROCEDURE [dbo].[stp_test_underscore_test]
(
 @str_Date_FROM NVARCHAR(20),
 @str_date_to NVARCHAR(20)
)
AS
SELECT  [codeObjectNo] AS code_object_no,
        [applicationNo] AS application_no
FROM    [dbo].[CodeObject];

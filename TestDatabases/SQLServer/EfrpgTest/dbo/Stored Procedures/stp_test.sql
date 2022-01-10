
CREATE PROCEDURE [dbo].[stp_test]
(
 @strDateFROM NVARCHAR(20),
 @strDateTo NVARCHAR(20),
 @retBool BIT OUTPUT
)
 AS
 DECLARE @intError INT
 SET @intError = 0
 SET @retBool = 0

 SELECT [codeObjectNo],
        [applicationNo],
        [type],
        [eName],
        [aName],
        [description],
        [codeName],
        [note],
        [isObject],
        [versionNumber]
 FROM   [dbo].[CodeObject];

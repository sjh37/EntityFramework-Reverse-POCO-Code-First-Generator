CREATE PROCEDURE [dbo].[stp_multiple_results_with_params] (@first_val INT, @second_val int NULL)
AS
SELECT  [codeObjectNo],[applicationNo] FROM    [dbo].[CodeObject];
SELECT * FROM Colour;

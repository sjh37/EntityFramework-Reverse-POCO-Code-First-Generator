CREATE PROCEDURE [dbo].[stp_multiple_multiple_results_with_params] (@first_val INT, @second_val int NULL, @third_val INT)
AS
	SELECT  [codeObjectNo],[applicationNo] FROM    [dbo].[CodeObject];
	SELECT * FROM Colour;
	SELECT * FROM BatchTest;
	SELECT * FROM Burak1;
	SELECT * FROM Car;
	SELECT * FROM AB_OrderLinesAB_;

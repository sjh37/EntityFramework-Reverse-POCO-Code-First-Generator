CREATE PROCEDURE [dbo].[stp_multiple_identical_results] (@someVar INT)
AS
	IF(@someVar > 5) BEGIN
		SELECT * FROM Colour;
	END ELSE BEGIN
		SELECT * FROM Colour;
	END

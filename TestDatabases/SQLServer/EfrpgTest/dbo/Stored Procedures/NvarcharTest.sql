


--- From WisdomGuidedByExperience
-- stored proc with nvarchar(max) parameter is missing size parameter
CREATE PROCEDURE [dbo].NvarcharTest @maxOutputParam NVARCHAR(MAX), @normalOutputParam NVARCHAR(20)
AS
BEGIN
    SET @maxOutputParam = 'hello'
    SET @normalOutputParam = 'world'
END

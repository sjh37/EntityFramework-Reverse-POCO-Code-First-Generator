
CREATE PROC FFRS.cv_data(@maxId INT)
AS
SELECT BatchUID,
       CVID,
       CVName FROM FFRS.CV WHERE CVID < @maxId


CREATE PROC dbo.dbo_proc_data_from_ffrs(@maxId INT)
AS
SELECT BatchUID,
       CVID,
       CVName FROM FFRS.CV WHERE CVID < @maxId

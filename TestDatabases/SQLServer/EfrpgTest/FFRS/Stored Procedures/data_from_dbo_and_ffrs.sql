
CREATE PROC FFRS.data_from_dbo_and_ffrs
AS
SELECT Id, PrimaryColourId, CarMake, CVName FROM Car JOIN FFRS.CV ON Id = CVID

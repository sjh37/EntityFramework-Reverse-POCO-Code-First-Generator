
CREATE PROC dbo.dbo_proc_data_from_ffrs_and_dbo
AS
SELECT Id, PrimaryColourId, CarMake, CVName FROM Car JOIN FFRS.CV ON Id = CVID



-- From pinger. Reserved words in stored procs
CREATE PROC dbo.StupidStoredProcedureParams(@ReqType varchar(25),@Dept smallint,@Class smallint,@Item SMALLINT)
AS
BEGIN
  RETURN 0
END


-- DROP VIEW ComplexView
CREATE VIEW dbo.ComplexView
AS
	with cteLicenses as (
		select sum(case when sp.type = 0 and sp.aName <> 'test'    then cast(sp.applicationNo as int) else 0 end) as AccessWare
			 , sum(case when sp.type = 4                           then cast(sp.applicationNo as int) else 0 end) as AdvInventory
			 , sum(                                                     cast(sp.applicationNo as int))            as Test
		  from CodeObject sp where sp.isObject = 0
	)
		SELECT  ISNULL(LicenseType, '') AS LicenseType, [Count]
		FROM    cteLicenses UNPIVOT ( [Count] FOR LicenseType IN (AccessWare, Test) ) unpvt;

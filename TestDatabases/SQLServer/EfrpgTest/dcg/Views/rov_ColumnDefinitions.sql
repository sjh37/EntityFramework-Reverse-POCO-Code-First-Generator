CREATE VIEW [dcg].[rov_ColumnDefinitions]
AS
	SELECT
		isc.*,
		o.TYPE
	FROM
		INFORMATION_SCHEMA.COLUMNS AS isc
		INNER JOIN sys.objects AS o
			ON SCHEMA_NAME(o.SCHEMA_ID) = isc.TABLE_SCHEMA
			   AND o.NAME = isc.TABLE_NAME
	WHERE
		NOT isc.TABLE_SCHEMA IN ('dcg');

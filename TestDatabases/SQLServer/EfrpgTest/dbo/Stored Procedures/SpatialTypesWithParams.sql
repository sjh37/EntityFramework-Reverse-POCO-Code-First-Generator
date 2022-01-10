
-- #571 Stored procs returning spatial types not being mapped correctly
-- DROP PROCEDURE dbo.SpatialTypesWithParams
-- DROP PROCEDURE dbo.SpatialTypesNoParams
CREATE PROCEDURE dbo.SpatialTypesWithParams (@geometry GEOMETRY, @geography GEOGRAPHY)
AS
    SELECT  [$] AS Dollar, GeographyType, GeometryType FROM BringTheAction;

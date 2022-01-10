CREATE DEFAULT [dbo].[d_t_address_type_domain]
    AS 'A';


GO
EXECUTE sp_bindefault @defname = N'[dbo].[d_t_address_type_domain]', @objname = N'[dbo].[PropertyTypesToAdd].[defaultCheck]';


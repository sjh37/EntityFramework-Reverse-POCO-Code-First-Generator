CREATE TABLE [dbo].[FinancialInstitutionOffice] (
    [Code]                     UNIQUEIDENTIFIER NOT NULL,
    [FinancialInstitutionCode] UNIQUEIDENTIFIER NOT NULL,
    [OfficeName]               NVARCHAR (200)   NULL,
    CONSTRAINT [UniqueOfficeName_FinancialInstitutionOffice] UNIQUE NONCLUSTERED ([FinancialInstitutionCode] ASC, [OfficeName] ASC)
);


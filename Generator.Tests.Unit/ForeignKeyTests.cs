using System;
using System.Collections.Generic;
using System.Linq;
using Efrpg;
using Efrpg.Readers;
using Generator.Tests.Common;
using NUnit.Framework;

namespace Generator.Tests.Unit
{
    [TestFixture, NonParallelizable]
    [Category(Constants.CI)]
    public class ForeignKeyTests
    {
        private List<RawForeignKey> rawForeignKeys;
        private List<RawIndex> rawIndexes;
        private List<Table> tables;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            rawForeignKeys = new List<RawForeignKey>
            {
                new RawForeignKey("FK_A_A", null, null, "C1", "C1", "dbo", "AAREF", "dbo", "A", 1, false, false, false),
                new RawForeignKey("FK_A_A", null, null, "C2", "C2", "dbo", "AAREF", "dbo", "A", 2, false, false, false),
                new RawForeignKey("AB_OrderLinesAB_FK", null, null, "ID", "OrderID", "dbo", "AB_OrdersAB_", "dbo", "AB_OrderLinesAB_", 1, false, false, false),
                new RawForeignKey("FK_Attendee_PhoneCountry", null, null, "CountryID", "PhoneCountryID", "dbo", "Country", "dbo", "Attendee", 1, false, false, false),
                new RawForeignKey("FK_BlahBlahLink_Blah", null, null, "BlahID", "BlahID", "dbo", "Blah", "dbo", "BlahBlahLink", 1, false, false, false),
                new RawForeignKey("FK_BlahBlahLink_Blah2", null, null, "BlahID", "BlahID2", "dbo", "Blah", "dbo", "BlahBlahLink", 1, false, false, false),
                new RawForeignKey("FK_BlahBlahLink_Blah_ro", null, null, "BlahID", "BlahID", "dbo", "Blah", "dbo", "BlahBlahLink_readonly", 1, false, false, false),
                new RawForeignKey("FK_BlahBlahLink_Blah_ro2", null, null, "BlahID", "BlahID2", "dbo", "Blah", "dbo", "BlahBlahLink_readonly", 1, false, false, false),
                new RawForeignKey("FK_BlahBlahLinkv2_Blah_ro", null, null, "BlahID", "BlahID", "dbo", "Blah", "dbo", "BlahBlahLink_v2", 1, true, false, false),
                new RawForeignKey("FK_BlahBlahLinkv2_Blah_ro2", null, null, "BlahID", "BlahID2", "dbo", "Blah", "dbo", "BlahBlahLink_v2", 1, false, false, false),
                new RawForeignKey("FK_BlahBlargLink_Blah", null, null, "BlahID", "BlahID", "dbo", "Blah", "dbo", "BlahBlargLink", 1, false, false, false),
                new RawForeignKey("FK_BlahBlargLink_Blarg", null, null, "BlargID", "BlargID", "dbo", "Blarg", "dbo", "BlahBlargLink", 1, false, false, false),
                new RawForeignKey("FK_Burak_Test2", null, null, "id", "id", "dbo", "Burak2", "dbo", "Burak1", 1, false, false, false),
                new RawForeignKey("FK_Burak_Test1", null, null, "id", "id_t", "dbo", "Burak2", "dbo", "Burak1", 1, false, false, false),
                new RawForeignKey("FK_Burak_Test1", null, null, "num", "num", "dbo", "Burak2", "dbo", "Burak1", 2, false, false, false),
                new RawForeignKey("FK_Burak_Test2", null, null, "num", "num", "dbo", "Burak2", "dbo", "Burak1", 2, false, false, false),
                new RawForeignKey("CarPrimaryColourFK", null, null, "Id", "PrimaryColourId", "dbo", "Colour", "dbo", "Car", 1, false, false, false),
                new RawForeignKey("CarToColour_CarId", null, null, "Id", "CarId", "dbo", "Car", "dbo", "CarToColour", 1, false, false, false),
                new RawForeignKey("CarToColour_ColourId", null, null, "Id", "ColourId", "dbo", "Colour", "dbo", "CarToColour", 1, false, false, false),
                new RawForeignKey("FK_CMS_FileTag_CMS_File", null, null, "FileId", "FileId", "dbo", "CMS_File", "dbo", "CMS_FileTag", 1, false, false, false),
                new RawForeignKey("FK_CMS_FileTag_CMS_Tag", null, null, "TagId", "TagId", "dbo", "CMS_Tag", "dbo", "CMS_FileTag", 1, false, false, false),
                new RawForeignKey("FK_CODE_MeetingTopicDetails_CODE_PARAM_MeetingTopicDetailSource", null, null, "Code", "origem", "dbo", "CODE_PARAM_MeetingTopicDetailSource", "dbo", "CODE_MeetingTopicDetails", 1, false, false, false),
                new RawForeignKey("FK_Company_CompanyGroup", null, null, "cogID", "cocogID", "dbo", "CompanyGroup", "dbo", "Company", 1, false, false, false),
                new RawForeignKey("FK_EventProcessorEventFilter__EventProcessor", null, null, "Id", "EventProcessorId", "dbo", "EventProcessor", "dbo", "EventProcessorEventFilter", 1, false, false, false),
                new RawForeignKey("fooderFK", null, null, "ID", "ID", "dbo", "header", "dbo", "footer", 1, false, false, false),
                new RawForeignKey("fooderFK", null, null, "anotherID", "otherID", "dbo", "header", "dbo", "footer", 2, false, false, false),
                new RawForeignKey("FK_ForeignKeyIsNotEnforcedItem_notnull_notnull", null, null, "not_null_value", "not_null_value", "dbo", "ForeignKeyIsNotEnforced", "dbo", "ForeignKeyIsNotEnforcedItem", 1, false, false, false),
                new RawForeignKey("FK_ForeignKeyIsNotEnforcedItem_notnull_null", null, null, "null_value", "not_null_value", "dbo", "ForeignKeyIsNotEnforced", "dbo", "ForeignKeyIsNotEnforcedItem", 1, false, false, false),
                new RawForeignKey("FK_ForeignKeyIsNotEnforcedItem_null_notnull", null, null, "not_null_value", "null_value", "dbo", "ForeignKeyIsNotEnforced", "dbo", "ForeignKeyIsNotEnforcedItem", 1, false, false, false),
                new RawForeignKey("FK_ForeignKeyIsNotEnforcedItem_null_null", null, null, "null_value", "null_value", "dbo", "ForeignKeyIsNotEnforced", "dbo", "ForeignKeyIsNotEnforcedItem", 1, false, false, false),
                new RawForeignKey("FK_Harish", null, null, "id", "another_id", "dbo", "PropertyTypesToAdd", "Beta", "Harish3485", 1, false, false, false),
                new RawForeignKey("FK_Harish", null, null, "KateID", "harish_id", "Kate", "ScreamAndShout", "Alpha", "Harish3485", 1, false, false, false),
                new RawForeignKey("FK_HasPrincipalKey_AB", null, null, "A", "A", "dbo", "HasPrincipalKeyTestParent", "dbo", "HasPrincipalKeyTestChild", 1, false, false, false),
                new RawForeignKey("FK_HasPrincipalKey_AC", null, null, "A", "A", "dbo", "HasPrincipalKeyTestParent", "dbo", "HasPrincipalKeyTestChild", 1, false, false, false),
                new RawForeignKey("FK_HasPrincipalKey_AB", null, null, "B", "B", "dbo", "HasPrincipalKeyTestParent", "dbo", "HasPrincipalKeyTestChild", 2, false, false, false),
                new RawForeignKey("FK_HasPrincipalKey_AC", null, null, "C", "C", "dbo", "HasPrincipalKeyTestParent", "dbo", "HasPrincipalKeyTestChild", 2, false, false, false),
                new RawForeignKey("FK_HasPrincipalKey_CD", null, null, "C", "C", "dbo", "HasPrincipalKeyTestParent", "dbo", "HasPrincipalKeyTestChild", 1, false, false, false),
                new RawForeignKey("FK_HasPrincipalKey_CD", null, null, "D", "D", "dbo", "HasPrincipalKeyTestParent", "dbo", "HasPrincipalKeyTestChild", 2, false, false, false),
                new RawForeignKey("FK_Issue_UploadedFileConsentDocument", null, null, "Id", "ConsentDocumentId", "OneEightSix", "UploadedFile", "OneEightSix", "Issue", 1, false, false, false),
                new RawForeignKey("FK_IssueUploadedFile_Issue", null, null, "Id", "IssueId", "OneEightSix", "Issue", "OneEightSix", "IssueUploadedFile", 1, false, false, false),
                new RawForeignKey("FK_IssueUploadedFile_UploadedFile", null, null, "Id", "UploadedFileId", "OneEightSix", "UploadedFile", "OneEightSix", "IssueUploadedFile", 1, false, false, false),
                new RawForeignKey("FK_PersonPosts_CreatedBy", null, null, "Id", "CreatedBy", "dbo", "Person", "dbo", "PersonPosts", 1, false, false, false),
                new RawForeignKey("FK_PersonPosts_UpdatedBy", null, null, "Id", "UpdatedBy", "dbo", "Person", "dbo", "PersonPosts", 1, false, false, false),
                new RawForeignKey("KateFK", null, null, "id", "KateID", "dbo", "ScreamAndShout", "Kate", "ScreamAndShout", 1, false, false, false),
                new RawForeignKey("space1FK", null, null, "id", "id", "dbo", "table with space", "dbo", "table mapping with space", 1, false, false, false),
                new RawForeignKey("space2FK", null, null, "id value", "id value", "dbo", "table with space and in columns", "dbo", "table mapping with space", 1, false, false, false),
                new RawForeignKey("tblOrdersFK", null, null, "ID", "OrderID", "dbo", "tblOrders", "dbo", "tblOrderLines", 1, false, false, false),
                new RawForeignKey("FK_Ticket_AppUser", null, null, "Id", "CreatedById", "dbo", "AppUser", "dbo", "Ticket", 1, false, false, false),
                new RawForeignKey("FK_Ticket_AppUser1", null, null, "Id", "ModifiedById", "dbo", "AppUser", "dbo", "Ticket", 1, false, false, false),
                new RawForeignKey("BetaToAlpha_AlphaWorkflow", null, null, "Id", "AlphaId", "Alpha", "workflow", "Beta", "ToAlpha", 1, false, false, false),
                new RawForeignKey("FK_User_Document_User1", null, null, "ID", "CreatedByUserID", "dbo", "User", "dbo", "User_Document", 1, false, false, false),
                new RawForeignKey("FK_User_Document_User", null, null, "ID", "UserID", "dbo", "User", "dbo", "User_Document", 1, true, false, false),
                new RawForeignKey("FK_User309_PhoneCountry", null, null, "CountryID", "PhoneCountryID", "dbo", "Country", "dbo", "User309", 1, false, false, false),
                new RawForeignKey("Issue47_UserRoles_roleid", null, null, "RoleId", "RoleId", "Issue47", "Role", "Issue47", "UserRoles", 1, false, false, false),
                new RawForeignKey("Issue47_UserRoles_userid", null, null, "UserId", "UserId", "Issue47", "Users", "Issue47", "UserRoles", 1, false, false, false)
            };

            rawIndexes = new List<RawIndex>
            {
                new RawIndex("dbo", "A", "PK_A", 1, "AId", 1, true, true, false, true),
                new RawIndex("dbo", "AAREF", "PK_AREF", 1, "C1", 2, true, true, false, true),
                new RawIndex("dbo", "AAREF", "PK_AREF", 2, "C2", 2, true, true, false, true),
                new RawIndex("dbo", "AB_OrderLinesAB_", "PK__AB_Order__3214EC2731B53C8A", 1, "ID", 1, true, true, false, true),
                new RawIndex("dbo", "AB_OrdersAB_", "PK__AB_Order__3214EC277606C4E4", 1, "ID", 1, true, true, false, true),
                new RawIndex("dbo", "AppUser", "PK__AppUser__3214EC074EE50760", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "Attendee", "PK_Attendee", 1, "AttendeeID", 1, true, true, false, true),
                new RawIndex("dbo", "BatchTest", "PK__BatchTes__357D4CF8F557E292", 1, "code", 1, true, true, false, true),
                new RawIndex("dbo", "BITFIDDLERALLCAPS", "PK__BITFIDDL__3214EC073CF127E7", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "BitFiddlerCATEGORIES", "PK__BitFiddl__3214EC07A08CA919", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "BitFiddlerCURRENCIES", "PK__BitFiddl__3214EC07D56DB345", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "Blah", "PK_Blah", 1, "BlahID", 1, true, true, false, true),
                new RawIndex("dbo", "BlahBlahLink", "PK_BlahBlahLink", 1, "BlahID", 2, true, true, false, true),
                new RawIndex("dbo", "BlahBlahLink", "PK_BlahBlahLink", 2, "BlahID2", 2, true, true, false, true),
                new RawIndex("dbo", "BlahBlahLink_readonly", "PK_BlahBlahLink_ro", 1, "BlahID", 2, true, true, false, true),
                new RawIndex("dbo", "BlahBlahLink_readonly", "PK_BlahBlahLink_ro", 2, "BlahID2", 2, true, true, false, true),
                new RawIndex("dbo", "BlahBlahLink_v2", "PK_BlahBlahLinkv2_ro", 1, "BlahID", 2, true, true, false, true),
                new RawIndex("dbo", "BlahBlahLink_v2", "PK_BlahBlahLinkv2_ro", 2, "BlahID2", 2, true, true, false, true),
                new RawIndex("dbo", "BlahBlargLink", "PK_BlahBlargLink", 1, "BlahID", 2, true, true, false, true),
                new RawIndex("dbo", "BlahBlargLink", "PK_BlahBlargLink", 2, "BlargID", 2, true, true, false, true),
                new RawIndex("dbo", "Blarg", "PK_Blarg", 1, "BlargID", 1, true, true, false, true),
                new RawIndex("dbo", "ColumnNameAndTypes", "PK_ColumnNameAndTypes", 1, "$", 1, true, true, false, true),
                new RawIndex("dbo", "Burak1", "PK_Burak1", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "Burak2", "PK_Burak2", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "Burak2", "U_Burak2", 1, "id", 2, true, false, true, false),
                new RawIndex("dbo", "Burak2", "U_Burak2", 2, "num", 2, true, false, true, false),
                new RawIndex("dbo", "Car", "PK__Car__3214EC0702833709", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "CarToColour", "PK__CarToCol__8C02E66BFA508402", 1, "CarId", 2, true, true, false, true),
                new RawIndex("dbo", "CarToColour", "PK__CarToCol__8C02E66BFA508402", 2, "ColourId", 2, true, true, false, true),
                new RawIndex("dbo", "ClientCreationState", "PK__ClientCr__3213E83F95DD2206", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "CMS_File", "PK_CMS_File", 1, "FileId", 1, true, true, false, true),
                new RawIndex("dbo", "CMS_Tag", "PK_CMS_Tag", 1, "TagId", 1, true, true, false, true),
                new RawIndex("dbo", "CODE_MeetingTopicDetails", "PK_CODE_MeetingTopicDetails", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "CODE_PARAM_MeetingTopicDetailSource", "PK_CODE_PARAM_MeetingTopicDetailSource", 1, "ID", 1, true, true, false, true),
                new RawIndex("dbo", "CODE_PARAM_MeetingTopicDetailSource", "UK_CODE_PARAM_MeetingTopicDetailSource", 1, "Code", 1, true, false, true, false),
                new RawIndex("dbo", "CodeObject", "aaaaaObject_PK", 1, "codeObjectNo", 1, true, true, false, false),
                new RawIndex("dbo", "Colour", "PK__Colour__3214EC07379EAA64", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "Company", "PK_Company", 1, "coID", 1, true, true, false, true),
                new RawIndex("dbo", "CompanyGroup", "PK_CompanyGroup", 1, "cogID", 1, true, true, false, true),
                new RawIndex("dbo", "Country", "PK_Country", 1, "CountryID", 1, true, true, false, true),
                new RawIndex("dbo", "DefaultCheckForNull", "PK__DefaultC__3214EC0723541C72", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "DSOpe", "PK__DSOpe__3214EC27150C693D", 1, "ID", 1, true, true, false, true),
                new RawIndex("dbo", "EnumWithDefaultValue", "PK__EnumWith__3214EC07A2120E43", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "EventProcessor", "PK_EventProcessor", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "EventProcessorEventFilter", "IX_EventProcessorEventFilter", 1, "EventProcessorId", 2, true, false, false, false),
                new RawIndex("dbo", "EventProcessorEventFilter", "IX_EventProcessorEventFilter", 2, "WantedEventId", 2, true, false, false, false),
                new RawIndex("dbo", "EventProcessorEventFilter", "PK_EventProcessorEventFilter", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "FinancialInstitutionOffice", "UniqueOfficeName_FinancialInstitutionOffice", 1, "FinancialInstitutionCode", 2, true, false, true, false),
                new RawIndex("dbo", "FinancialInstitutionOffice", "UniqueOfficeName_FinancialInstitutionOffice", 2, "OfficeName", 2, true, false, true, false),
                new RawIndex("dbo", "footer", "PK__footer__3214EC272B429812", 1, "ID", 1, true, true, false, true),
                new RawIndex("dbo", "ForeignKeyIsNotEnforced", "PK__ForeignK__3213E83F7A9FD04F", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "ForeignKeyIsNotEnforced", "UQ_ForeignKeyIsNotEnforced_not_null_value", 1, "not_null_value", 1, true, false, true, false),
                new RawIndex("dbo", "ForeignKeyIsNotEnforced", "UQ_ForeignKeyIsNotEnforced_null_value", 1, "null_value", 1, true, false, true, false),
                new RawIndex("dbo", "ForeignKeyIsNotEnforcedItem", "PK__ForeignK__3213E83FB7C5760B", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "ForeignKeyIsNotEnforcedItem", "UQ_ForeignKeyIsNotEnforcedItem_not_null_value", 1, "not_null_value", 1, true, false, true, false),
                new RawIndex("dbo", "ForeignKeyIsNotEnforcedItem", "UQ_ForeignKeyIsNotEnforcedItem_null_value", 1, "null_value", 1, true, false, true, false),
                new RawIndex("Beta", "Harish3485", "PK__Harish34__3213E83F1B89ADF0", 1, "id", 1, true, true, false, true),
                new RawIndex("Alpha", "Harish3485", "PK__Harish34__3213E83F6B711DC1", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "HasPrincipalKeyTestChild", "PK__HasPrinc__3214EC07061FCDAA", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "HasPrincipalKeyTestParent", "PK__HasPrinc__3214EC07C4D24B68", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "HasPrincipalKeyTestParent", "UQ_HasPrincipalKeyTestParent_AB", 1, "A", 2, true, false, true, false),
                new RawIndex("dbo", "HasPrincipalKeyTestParent", "UQ_HasPrincipalKeyTestParent_AB", 2, "B", 2, true, false, true, false),
                new RawIndex("dbo", "HasPrincipalKeyTestParent", "UQ_HasPrincipalKeyTestParent_AC", 1, "A", 2, true, false, true, false),
                new RawIndex("dbo", "HasPrincipalKeyTestParent", "UQ_HasPrincipalKeyTestParent_AC", 2, "C", 2, true, false, true, false),
                new RawIndex("dbo", "HasPrincipalKeyTestParent", "UQ_HasPrincipalKeyTestParent_CD", 1, "C", 2, true, false, true, false),
                new RawIndex("dbo", "HasPrincipalKeyTestParent", "UQ_HasPrincipalKeyTestParent_CD", 2, "D", 2, true, false, true, false),
                new RawIndex("dbo", "header", "PK__header__FAB049E7232129AF", 1, "ID", 2, true, true, false, true),
                new RawIndex("dbo", "header", "PK__header__FAB049E7232129AF", 2, "anotherID", 2, true, true, false, true),
                new RawIndex("dbo", "hierarchy_test", "PK__hierarch__3214EC27A00D9B63", 1, "ID", 1, true, true, false, true),
                new RawIndex("OneEightSix", "Issue", "PK_Issue", 1, "Id", 1, true, true, false, true),
                new RawIndex("OneEightSix", "IssueUploadedFile", "PK_IssueUploadedFile", 1, "UploadedFileId", 2, true, true, false, true),
                new RawIndex("OneEightSix", "IssueUploadedFile", "PK_IssueUploadedFile", 2, "IssueId", 2, true, true, false, true),
                new RawIndex("dbo", "MultipleKeys", "IX_MultipleKeys_BestHolidayType", 1, "BestHolidayTypeId", 1, false, false, false, false),
                new RawIndex("dbo", "MultipleKeys", "IX_MultipleKeys_Holiday_Bank", 1, "BestHolidayTypeId", 2, true, false, false, false),
                new RawIndex("dbo", "MultipleKeys", "IX_MultipleKeys_Holiday_Bank", 2, "BankId", 2, true, false, false, false),
                new RawIndex("dbo", "MultipleKeys", "PK_MultipleKeys", 1, "UserId", 3, true, true, false, true),
                new RawIndex("dbo", "MultipleKeys", "PK_MultipleKeys", 2, "FavouriteColourId", 3, true, true, false, true),
                new RawIndex("dbo", "MultipleKeys", "PK_MultipleKeys", 3, "BestHolidayTypeId", 3, true, true, false, true),
                new RawIndex("dbo", "MultipleKeys", "UC_MultipleKeys_FavouriteColour", 1, "FavouriteColourId", 1, true, false, true, false),
                new RawIndex("dbo", "Period.Table", "PK__Period.T__3213E83F3E171D68", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "PeriodTestTable", "PK__PeriodTe__3213E83F590BD2DE", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "Person", "PK__Person__3214EC07424FC866", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "PersonPosts", "PK__PersonPo__3214EC0714F40FA4", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "pk_ordinal_test", "PK__pk_ordin__1135D3B4AF17DB21", 2, "C1", 2, true, true, false, true),
                new RawIndex("dbo", "pk_ordinal_test", "PK__pk_ordin__1135D3B4AF17DB21", 1, "C3", 2, true, true, false, true),
                new RawIndex("dbo", "PropertyTypesToAdd", "PK__Property__3213E83FCECB1764", 1, "id", 1, true, true, false, true),
                new RawIndex("Issue47", "Role", "PK__Role__8AFACE1A223A2B5D", 1, "RoleId", 1, true, true, false, true),
                new RawIndex("dbo", "ScreamAndShout", "PK__ScreamAn__3213E83F55C3020B", 1, "id", 1, true, true, false, true),
                new RawIndex("Kate", "ScreamAndShout", "PK__ScreamAn__8540EE1FFC54DDC8", 1, "KateID", 1, true, true, false, true),
                new RawIndex("dbo", "StockPrediction", "PK__StockPre__3213E83FFBA52754", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "table mapping with space", "PK_TableMappingWithSpace", 1, "id", 2, true, true, false, true),
                new RawIndex("dbo", "table mapping with space", "PK_TableMappingWithSpace", 2, "id value", 2, true, true, false, true),
                new RawIndex("dbo", "table with duplicate column names", "PK__table wi__3213E83F7E62FED9", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "table with space", "PK__table wi__3213E83F6330D784", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "table with space and in columns", "PK__table wi__92CF061C44B1B06D", 1, "id value", 1, true, true, false, true),
                new RawIndex("dbo", "table.with.multiple.periods", "PK__table.wi__3213E83F0D3FCA9C", 1, "id", 1, true, true, false, true),
                new RawIndex("dbo", "TableWithSpaceInColumnOnly", "PK__TableWit__92CF061C3D75F56A", 1, "id value", 1, true, true, false, true),
                new RawIndex("dbo", "TadeuszSobol", "PK__TadeuszS__3214EC07437DCB5A", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "tblOrderErrors", "PK__tblOrder__3214EC27BAF60861", 1, "ID", 1, true, true, false, true),
                new RawIndex("dbo", "tblOrderErrorsAB_", "PK__tblOrder__3214EC2777E38424", 1, "ID", 1, true, true, false, true),
                new RawIndex("dbo", "tblOrderLines", "PK__tblOrder__3214EC2741C7C0E7", 1, "ID", 1, true, true, false, true),
                new RawIndex("dbo", "tblOrders", "PK__tblOrder__3214EC27DE21AFD8", 1, "ID", 1, true, true, false, true),
                new RawIndex("dbo", "Ticket", "PK_Ticket", 1, "Id", 1, true, true, false, true),
                new RawIndex("Beta", "ToAlpha", "PK__ToAlpha__3214EC07B6C8B1E7", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "Token", "PK__Token__3214EC0780725089", 1, "Id", 1, true, true, false, true),
                new RawIndex("OneEightSix", "UploadedFile", "PK_UploadedFile", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "User", "PK_User", 1, "ID", 1, true, true, false, true),
                new RawIndex("dbo", "User_Document", "PK_User_Document", 1, "ID", 1, true, true, false, true),
                new RawIndex("dbo", "User309", "PK_User309", 1, "UserID", 1, true, true, false, true),
                new RawIndex("Issue47", "UserRoles", "PK__UserRole__3D978A3528CA58C0", 1, "UserRoleId", 1, true, true, false, true),
                new RawIndex("Issue47", "Users", "PK__Users__1788CC4CFC98EC84", 1, "UserId", 1, true, true, false, true),
                new RawIndex("Alpha", "workflow", "PK__workflow__3214EC072804087E", 1, "Id", 1, true, true, false, true),
                new RawIndex("Beta", "workflow", "PK__workflow__3214EC07906CBEB9", 1, "Id", 1, true, true, false, true),
                new RawIndex("dbo", "Бренды товара", "PK_Бренды", 1, "Код бренда", 1, true, true, false, true)
            };
        }

        [SetUp]
        public void SetUp()
        {
            tables = new List<Table>(10);
        }

        [Test]
        public void IdentifyUniqueForeignKeys()
        {
            Efrpg.Generators.Generator.IdentifyUniqueForeignKeys(rawIndexes, rawForeignKeys);

            Assert.AreEqual(15, rawForeignKeys.Count(x => x.HasUniqueConstraint));

            var set = rawForeignKeys
                .Where(x => x.HasUniqueConstraint)
                .OrderBy(x => x.FkColumn)
                .ThenBy(x => x.PkColumn)
                .ThenBy(x => x.Ordinal)
                .ToArray();

            var expected = new[]
            {
                new { Fk = "A", Pk = "A", Constraint = "FK_HasPrincipalKey_AB", Ordinal = 1 },
                new { Fk = "A", Pk = "A", Constraint = "FK_HasPrincipalKey_AC", Ordinal = 1 },
                new { Fk = "B", Pk = "B", Constraint = "FK_HasPrincipalKey_AB", Ordinal = 2 },
                new { Fk = "C", Pk = "C", Constraint = "FK_HasPrincipalKey_CD", Ordinal = 1 },
                new { Fk = "C", Pk = "C", Constraint = "FK_HasPrincipalKey_AC", Ordinal = 2 },
                new { Fk = "D", Pk = "D", Constraint = "FK_HasPrincipalKey_CD", Ordinal = 2 },
                new { Fk = "id", Pk = "id", Constraint = "FK_Burak_Test2", Ordinal = 1 },
                new { Fk = "id_t", Pk = "id", Constraint = "FK_Burak_Test1", Ordinal = 1 },
                new { Fk = "not_null_value", Pk = "not_null_value", Constraint = "FK_ForeignKeyIsNotEnforcedItem_notnull_notnull", Ordinal = 1 },
                new { Fk = "not_null_value", Pk = "null_value", Constraint = "FK_ForeignKeyIsNotEnforcedItem_notnull_null", Ordinal = 1 },
                new { Fk = "null_value", Pk = "not_null_value", Constraint = "FK_ForeignKeyIsNotEnforcedItem_null_notnull", Ordinal = 1 },
                new { Fk = "null_value", Pk = "null_value", Constraint = "FK_ForeignKeyIsNotEnforcedItem_null_null", Ordinal = 1 },
                new { Fk = "num", Pk = "num", Constraint = "FK_Burak_Test1", Ordinal = 2 },
                new { Fk = "num", Pk = "num", Constraint = "FK_Burak_Test2", Ordinal = 2 },
                new { Fk = "origem", Pk = "Code", Constraint = "FK_CODE_MeetingTopicDetails_CODE_PARAM_MeetingTopicDetailSource", Ordinal = 1 },
            };

            for (var n = 0; n < expected.Length; n++)
                Console.WriteLine($"{set[n].FkColumn}\t{set[n].PkColumn}\t{set[n].ConstraintName}\t{set[n].Ordinal}");

            Assert.AreEqual(expected.Length, set.Length);

            for (var n = 0; n < expected.Length; n++)
            {
                Assert.AreEqual(expected[n].Fk, set[n].FkColumn);
                Assert.AreEqual(expected[n].Pk, set[n].PkColumn);
                Assert.AreEqual(expected[n].Constraint, set[n].ConstraintName);
                Assert.AreEqual(expected[n].Ordinal, set[n].Ordinal);
            }
        }

        [Test] // Always run all cases together as they build up ReverseNavigationUniquePropName
        // checkForFkNameClashes = true
        [TestCase("01", "Burak2", "Burak1", "Id|IdT|Num", true, "Burak2", true, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id")]
        [TestCase("02", "Burak1", "Burak2", "Id|Num", false, "Burak1", true, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id")]
        [TestCase("03", "Burak2", "Burak1", "Id|IdT|Num", true, "Burak2", true, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id_t")]
        [TestCase("04", "Burak1", "Burak2", "Id|Num", false, "Burak1", true, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id_t")]
        [TestCase("05", "Car", "Colour", "Id|Name", false, "Car", true, false, Relationship.OneToMany, "Car", "Colour", true, "PrimaryColourId")]
        [TestCase("06", "Car", "CarToColour", "CarId|ColourId", true, "Car", true, true, Relationship.ManyToOne, "CarToColour", "Car", true, "CarId")]
        [TestCase("07", "CarToColour", "Car", "Id|PrimaryColourId|CarMake|ComputedColumn|ComputedColumnPersisted", false, "CarToColour", true, false, Relationship.OneToMany, "CarToColour", "Car", true, "CarId")]
        [TestCase("08", "CarToColour", "Colour", "Id|Name", false, "CarToColour", true, false, Relationship.OneToMany, "CarToColour", "Colour", true, "ColourId")]
        [TestCase("09", "User", "UserDocument", "Id|UserId|CreatedByUserId", true, "User", true, true, Relationship.ManyToOne, "User_Document", "User", true, "CreatedByUserID")]
        [TestCase("10", "UserDocument", "User", "Id|ExternalUserId", false, "UserDocument", true, false, Relationship.OneToMany, "User_Document", "User", true, "CreatedByUserID")]
        [TestCase("11", "User", "UserDocument", "Id|UserId|CreatedByUserId", true, "User", true, true, Relationship.ManyToOne, "User_Document", "User", true, "UserID")]
        [TestCase("12", "UserDocument", "User", "Id|ExternalUserId", false, "UserDocument", true, false, Relationship.OneToMany, "User_Document", "User", true, "UserID")]
        // checkForFkNameClashes = false
        [TestCase("13", "Burak2", "Burak1", "Id|IdT|Num", true, "Burak2", false, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id")]
        [TestCase("14", "Burak1", "Burak2", "Id|Num", false, "Burak1", false, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id")]
        [TestCase("15", "Burak2", "Burak1", "Id|IdT|Num", true, "Burak2", false, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id_t")]
        [TestCase("16", "Burak1", "Burak2", "Id|Num", false, "Burak1", false, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id_t")]
        [TestCase("17", "Car", "Colour", "Id|Name", false, "Car", false, false, Relationship.OneToMany, "Car", "Colour", true, "PrimaryColourId")]
        [TestCase("18", "Car", "CarToColour", "CarId|ColourId", true, "Car", false, true, Relationship.ManyToOne, "CarToColour", "Car", true, "CarId")]
        [TestCase("19", "CarToColour", "Car", "Id|PrimaryColourId|CarMake|ComputedColumn|ComputedColumnPersisted", false, "CarToColour", false, false, Relationship.OneToMany, "CarToColour", "Car", true, "CarId")]
        [TestCase("20", "CarToColour", "Colour", "Id|Name", false, "CarToColour", false, false, Relationship.OneToMany, "CarToColour", "Colour", true, "ColourId")]
        [TestCase("21", "User", "UserDocument", "Id|UserId|CreatedByUserId", true, "User", false, true, Relationship.ManyToOne, "User_Document", "User", true, "CreatedByUserID")]
        [TestCase("22", "UserDocument", "User", "Id|ExternalUserId", false, "UserDocument", false, false, Relationship.OneToMany, "User_Document", "User", true, "CreatedByUserID")]
        [TestCase("23", "User", "UserDocument", "Id|UserId|CreatedByUserId", true, "User", false, true, Relationship.ManyToOne, "User_Document", "User", true, "UserID")]
        [TestCase("24", "UserDocument", "User", "Id|ExternalUserId", false, "UserDocument", false, false, Relationship.OneToMany, "User_Document", "User", true, "UserID")]
        public void LegacyForeignKeyNames(string testOrder, string expected, string NameHumanCase, string columns, bool isParent, string tableNameHumanCase, bool checkForFkNameClashes,
            bool makeSingular, Relationship relationship, string fkTableName, string pkTableName, bool includeReverseNavigation, string fkColumn)
        {
            Console.WriteLine(testOrder); // Keep this field to make sure test cases run in order as it's important

            // Arrange
            var (table, foreignKey) = PrepareTest(NameHumanCase, columns, fkTableName, pkTableName, includeReverseNavigation, fkColumn, ForeignKeyNamingStrategy.Legacy);

            // Act
            var result = table.GetUniqueForeignKeyName(isParent, tableNameHumanCase, foreignKey, checkForFkNameClashes, makeSingular, relationship);

            // Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(ForeignKeyNamingStrategy.Legacy, Settings.ForeignKeyNamingStrategy);
        }

        /*[Test] // Always run all cases together as they build up ReverseNavigationUniquePropName
        // checkForFkNameClashes = true
        [TestCase("01", "Burak2", "Burak1", "Id|IdT|Num", true, "Burak2", true, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id")]
        [TestCase("02", "Burak1", "Burak2", "Id|Num", false, "Burak1", true, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id")]
        [TestCase("03", "Burak2", "Burak1", "Id|IdT|Num", true, "Burak2", true, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id_t")]
        [TestCase("04", "Burak1", "Burak2", "Id|Num", false, "Burak1", true, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id_t")]
        [TestCase("05", "Car", "Colour", "Id|Name", false, "Car", true, false, Relationship.OneToMany, "Car", "Colour", true, "PrimaryColourId")]
        [TestCase("06", "Car", "CarToColour", "CarId|ColourId", true, "Car", true, true, Relationship.ManyToOne, "CarToColour", "Car", true, "CarId")]
        [TestCase("07", "CarToColour", "Car", "Id|PrimaryColourId|CarMake|ComputedColumn|ComputedColumnPersisted", false, "CarToColour", true, false, Relationship.OneToMany, "CarToColour", "Car", true, "CarId")]
        [TestCase("08", "CarToColour", "Colour", "Id|Name", false, "CarToColour", true, false, Relationship.OneToMany, "CarToColour", "Colour", true, "ColourId")]
        [TestCase("09", "User", "UserDocument", "Id|UserId|CreatedByUserId", true, "User", true, true, Relationship.ManyToOne, "User_Document", "User", true, "CreatedByUserID")]
        [TestCase("10", "UserDocument", "User", "Id|ExternalUserId", false, "UserDocument", true, false, Relationship.OneToMany, "User_Document", "User", true, "CreatedByUserID")]
        [TestCase("11", "User", "UserDocument", "Id|UserId|CreatedByUserId", true, "User", true, true, Relationship.ManyToOne, "User_Document", "User", true, "UserID")]
        [TestCase("12", "UserDocument", "User", "Id|ExternalUserId", false, "UserDocument", true, false, Relationship.OneToMany, "User_Document", "User", true, "UserID")]
        // checkForFkNameClashes = false
        [TestCase("13", "Burak2", "Burak1", "Id|IdT|Num", true, "Burak2", false, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id")]
        [TestCase("14", "Burak1", "Burak2", "Id|Num", false, "Burak1", false, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id")]
        [TestCase("15", "Burak2", "Burak1", "Id|IdT|Num", true, "Burak2", false, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id_t")]
        [TestCase("16", "Burak1", "Burak2", "Id|Num", false, "Burak1", false, true, Relationship.OneToOne, "Burak1", "Burak2", true, "id_t")]
        [TestCase("17", "Car", "Colour", "Id|Name", false, "Car", false, false, Relationship.OneToMany, "Car", "Colour", true, "PrimaryColourId")]
        [TestCase("18", "Car", "CarToColour", "CarId|ColourId", true, "Car", false, true, Relationship.ManyToOne, "CarToColour", "Car", true, "CarId")]
        [TestCase("19", "CarToColour", "Car", "Id|PrimaryColourId|CarMake|ComputedColumn|ComputedColumnPersisted", false, "CarToColour", false, false, Relationship.OneToMany, "CarToColour", "Car", true, "CarId")]
        [TestCase("20", "CarToColour", "Colour", "Id|Name", false, "CarToColour", false, false, Relationship.OneToMany, "CarToColour", "Colour", true, "ColourId")]
        [TestCase("21", "User", "UserDocument", "Id|UserId|CreatedByUserId", true, "User", false, true, Relationship.ManyToOne, "User_Document", "User", true, "CreatedByUserID")]
        [TestCase("22", "UserDocument", "User", "Id|ExternalUserId", false, "UserDocument", false, false, Relationship.OneToMany, "User_Document", "User", true, "CreatedByUserID")]
        [TestCase("23", "User", "UserDocument", "Id|UserId|CreatedByUserId", true, "User", false, true, Relationship.ManyToOne, "User_Document", "User", true, "UserID")]
        [TestCase("24", "UserDocument", "User", "Id|ExternalUserId", false, "UserDocument", false, false, Relationship.OneToMany, "User_Document", "User", true, "UserID")]
        public void LatestForeignKeyNames(string testOrder, string expected, string NameHumanCase, string columns, bool isParent, string tableNameHumanCase, bool checkForFkNameClashes,
            bool makeSingular, Relationship relationship, string fkTableName, string pkTableName, bool includeReverseNavigation, string fkColumn)
        {
            Console.WriteLine(testOrder); // Keep this field to make sure test cases run in order as it's important

            // Arrange
            var (table, foreignKey) = PrepareTest(NameHumanCase, columns, fkTableName, pkTableName, includeReverseNavigation, fkColumn, ForeignKeyNamingStrategy.Latest);

            // Act
            var result = table.GetUniqueForeignKeyName(isParent, tableNameHumanCase, foreignKey, checkForFkNameClashes, makeSingular, relationship);

            // Assert
            Assert.AreEqual(expected, result);
            Assert.AreEqual(ForeignKeyNamingStrategy.Latest, Settings.ForeignKeyNamingStrategy);
        }*/

        private (Table table, ForeignKey foreignKey) PrepareTest(string NameHumanCase, string columns, string fkTableName, string pkTableName, bool includeReverseNavigation, string fkColumn, ForeignKeyNamingStrategy foreignKeyNamingStrategy)
        {
            var table = tables.FirstOrDefault(x => x.NameHumanCase == NameHumanCase);
            if (table == null)
            {
                Settings.ForeignKeyNamingStrategy = foreignKeyNamingStrategy;
                table = new Table(null, new Schema("dbo"), NameHumanCase, false)
                {
                    NameHumanCase = NameHumanCase
                };
                foreach (var col in columns.Split('|'))
                {
                    table.Columns.Add(new Column { NameHumanCase = col });
                }

                tables.Add(table);
            }

            var foreignKey = new ForeignKey(fkTableName, "dbo", pkTableName, "dbo", fkColumn, "", "", "", "", 1, false, false, "", "", true)
            {
                IncludeReverseNavigation = includeReverseNavigation
            };
            return (table, foreignKey);
        }
    }
}
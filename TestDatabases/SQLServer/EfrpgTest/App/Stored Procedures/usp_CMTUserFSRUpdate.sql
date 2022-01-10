CREATE PROCEDURE [App].[usp_CMTUserFSRUpdate]
    @userId INT,
    @fsrId INT,
    @ufsrId INT OUT
AS
	SET NOCOUNT ON
	DECLARE @appId INT
	SET @appId = 2
	INSERT  [UserFacilityServiceRole] SELECT  @userId, @appId, @fsrId
	SELECT  @ufsrId = @@IDENTITY

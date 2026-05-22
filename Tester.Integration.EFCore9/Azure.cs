using System.Collections.Generic;



// ------------------------------------------------------------------------------------------------
// WARNING: Failed to load provider "Microsoft.Data.SqlClient" - A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: TCP Provider, error: 0 - No such host is known.)
// Allowed providers:
//    "System.Data.Odbc"
//    "System.Data.OleDb"
//    "System.Data.OracleClient"
//    "System.Data.SqlClient"
//    "Oracle.ManagedDataAccess.Client"
//    "System.Data.SQLite"
//    "System.Data.SqlServerCe.4.0"
//    "Npgsql"
//    "MySql.Data.MySqlClient"
//    "Microsoft.SqlServerCe.Client.4.0"
//    "Microsoft.Data.SqlClient"

/*   at Microsoft.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction)
   at Microsoft.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose)
   at Microsoft.Data.SqlClient.TdsParser.Connect(ServerInfo serverInfo, SqlInternalConnectionTds connHandler, Boolean ignoreSniOpenTimeout, Int64 timerExpire, SqlConnectionString connectionOptions, Boolean withFailover, Boolean isFirstTransparentAttempt, ServerCertificateValidationCallback serverCallback, ClientCertificateRetrievalCallback clientCallback, Boolean useOriginalAddressInfo, Boolean disableTnir)
   at Microsoft.Data.SqlClient.SqlInternalConnectionTds.AttemptOneLogin(ServerInfo serverInfo, String newPassword, SecureString newSecurePassword, Boolean ignoreSniOpenTimeout, TimeoutTimer timeout, Boolean withFailover, Boolean isFirstTransparentAttempt, Boolean disableTnir)
   at Microsoft.Data.SqlClient.SqlInternalConnectionTds.LoginNoFailover(ServerInfo serverInfo, String newPassword, SecureString newSecurePassword, Boolean redirectedUserInstance, SqlConnectionString connectionOptions, SqlCredential credential, TimeoutTimer timeout)
   at Microsoft.Data.SqlClient.SqlInternalConnectionTds.OpenLoginEnlist(TimeoutTimer timeout, SqlConnectionString connectionOptions, SqlCredential credential, String newPassword, SecureString newSecurePassword, Boolean redirectedUserInstance)
   at Microsoft.Data.SqlClient.SqlInternalConnectionTds..ctor(DbConnectionPoolIdentity identity, SqlConnectionString connectionOptions, SqlCredential credential, Object providerInfo, String newPassword, SecureString newSecurePassword, Boolean redirectedUserInstance, SqlConnectionString userConnectionOptions, SessionData reconnectSessionData, ServerCertificateValidationCallback serverCallback, ClientCertificateRetrievalCallback clientCallback, DbConnectionPool pool, String accessToken, SqlClientOriginalNetworkAddressInfo originalNetworkAddressInfo, Boolean applyTransientFaultHandling)
   at Microsoft.Data.SqlClient.SqlConnectionFactory.CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, Object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
   at Microsoft.Data.ProviderBase.DbConnectionFactory.CreatePooledConnection(DbConnectionPool pool, DbConnection owningObject, DbConnectionOptions options, DbConnectionPoolKey poolKey, DbConnectionOptions userOptions)
   at Microsoft.Data.ProviderBase.DbConnectionPool.CreateObject(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
   at Microsoft.Data.ProviderBase.DbConnectionPool.UserCreateRequest(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
   at Microsoft.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, UInt32 waitForMultipleObjectsTimeout, Boolean allowCreate, Boolean onlyOneCheckConnection, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at Microsoft.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at Microsoft.Data.ProviderBase.DbConnectionFactory.TryGetConnection(DbConnection owningConnection, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, DbConnectionInternal& connection)
   at Microsoft.Data.ProviderBase.DbConnectionInternal.TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   at Microsoft.Data.SqlClient.SqlConnection.TryOpenInner(TaskCompletionSource`1 retry)
   at Microsoft.Data.SqlClient.SqlConnection.TryOpen(TaskCompletionSource`1 retry, SqlConnectionOverrides overrides)
   at Microsoft.Data.SqlClient.SqlConnection.Open(SqlConnectionOverrides overrides)
   at Microsoft.VisualStudio.TextTemplating35B6C0318B12DCA6DC9616E7040FDC8B719F492AA22CD81B0D51811679341BDDF70D63246271CA2DC53D5AB1545F8FF08944BD7B9254E49CD299E096887FC474.GeneratedTextTransformation.DatabaseReader.Init() in C:\S\Source (open source)\EntityFramework-Reverse-POCO-Code-First-Generator\Tester.Integration.EFCore9\..\EntityFramework.Reverse.POCO.Generator\EF.Reverse.POCO.v3.ttinclude:line 13761
   at Microsoft.VisualStudio.TextTemplating35B6C0318B12DCA6DC9616E7040FDC8B719F492AA22CD81B0D51811679341BDDF70D63246271CA2DC53D5AB1545F8FF08944BD7B9254E49CD299E096887FC474.GeneratedTextTransformation.SqlServerDatabaseReader.Init() in C:\S\Source (open source)\EntityFramework-Reverse-POCO-Code-First-Generator\Tester.Integration.EFCore9\..\EntityFramework.Reverse.POCO.Generator\EF.Reverse.POCO.v3.ttinclude:line 18045
   at Microsoft.VisualStudio.TextTemplating35B6C0318B12DCA6DC9616E7040FDC8B719F492AA22CD81B0D51811679341BDDF70D63246271CA2DC53D5AB1545F8FF08944BD7B9254E49CD299E096887FC474.GeneratedTextTransformation.Generator.Init(DatabaseReader databaseReader, String singleDbContextSubNamespace) in C:\S\Source (open source)\EntityFramework-Reverse-POCO-Code-First-Generator\Tester.Integration.EFCore9\..\EntityFramework.Reverse.POCO.Generator\EF.Reverse.POCO.v3.ttinclude:line 4953
   at Microsoft.VisualStudio.TextTemplating35B6C0318B12DCA6DC9616E7040FDC8B719F492AA22CD81B0D51811679341BDDF70D63246271CA2DC53D5AB1545F8FF08944BD7B9254E49CD299E096887FC474.GeneratedTextTransformation.GeneratorFactory.Create(FileManagementService fileManagementService, Type fileManagerType, String singleDbContextSubNamespace) in C:\S\Source (open source)\EntityFramework-Reverse-POCO-Code-First-Generator\Tester.Integration.EFCore9\..\EntityFramework.Reverse.POCO.Generator\EF.Reverse.POCO.v3.ttinclude:line 7353*/
// ------------------------------------------------------------------------------------------------




// ------------------------------------------------------------------------------------------------
// WARNING: Failed to load provider "System.Data.SqlClient" - Cannot open database "EfrpgTest" requested by the login. The login failed. Login failed for user 'AMR\rplemons'.
// Allowed providers:
//    "System.Data.Odbc"
//    "System.Data.OleDb"
//    "System.Data.OracleClient"
//    "System.Data.SqlClient"
//    "Microsoft.SqlServerCe.Client.4.0"
//    "Microsoft.Data.SqlClient"

/*   at System.Data.SqlClient.SqlInternalConnectionTds..ctor(DbConnectionPoolIdentity identity, SqlConnectionString connectionOptions, SqlCredential credential, Object providerInfo, String newPassword, SecureString newSecurePassword, Boolean redirectedUserInstance, SqlConnectionString userConnectionOptions, SessionData reconnectSessionData, DbConnectionPool pool, String accessToken, Boolean applyTransientFaultHandling, SqlAuthenticationProviderManager sqlAuthProviderManager)
   at System.Data.SqlClient.SqlConnectionFactory.CreateConnection(DbConnectionOptions options, DbConnectionPoolKey poolKey, Object poolGroupProviderInfo, DbConnectionPool pool, DbConnection owningConnection, DbConnectionOptions userOptions)
   at System.Data.ProviderBase.DbConnectionFactory.CreatePooledConnection(DbConnectionPool pool, DbConnection owningObject, DbConnectionOptions options, DbConnectionPoolKey poolKey, DbConnectionOptions userOptions)
   at System.Data.ProviderBase.DbConnectionPool.CreateObject(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
   at System.Data.ProviderBase.DbConnectionPool.UserCreateRequest(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
   at System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, UInt32 waitForMultipleObjectsTimeout, Boolean allowCreate, Boolean onlyOneCheckConnection, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionPool.TryGetConnection(DbConnection owningObject, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionFactory.TryGetConnection(DbConnection owningConnection, TaskCompletionSource`1 retry, DbConnectionOptions userOptions, DbConnectionInternal oldConnection, DbConnectionInternal& connection)
   at System.Data.ProviderBase.DbConnectionInternal.TryOpenConnectionInternal(DbConnection outerConnection, DbConnectionFactory connectionFactory, TaskCompletionSource`1 retry, DbConnectionOptions userOptions)
   at System.Data.SqlClient.SqlConnection.TryOpenInner(TaskCompletionSource`1 retry)
   at System.Data.SqlClient.SqlConnection.TryOpen(TaskCompletionSource`1 retry)
   at System.Data.SqlClient.SqlConnection.Open()
   at Microsoft.VisualStudio.TextTemplating737FAC30C3264AD8F42A0C259EBF8F2D2E4AC3073EA4DD78C59080608549DA334C3EBB41F237D1796F3D7F1AF63047B137AB4CB789A7C28D66622E1B9419550F.GeneratedTextTransformation.DatabaseReader.Init() in E:\DevRoot\Trash\EntityFramework-Reverse-POCO-Code-First-Generator\Tester.Integration.EfCore3\..\EntityFramework.Reverse.POCO.Generator\EF.Reverse.POCO.v3.ttinclude:line 12568
   at Microsoft.VisualStudio.TextTemplating737FAC30C3264AD8F42A0C259EBF8F2D2E4AC3073EA4DD78C59080608549DA334C3EBB41F237D1796F3D7F1AF63047B137AB4CB789A7C28D66622E1B9419550F.GeneratedTextTransformation.SqlServerDatabaseReader.Init() in E:\DevRoot\Trash\EntityFramework-Reverse-POCO-Code-First-Generator\Tester.Integration.EfCore3\..\EntityFramework.Reverse.POCO.Generator\EF.Reverse.POCO.v3.ttinclude:line 16531
   at Microsoft.VisualStudio.TextTemplating737FAC30C3264AD8F42A0C259EBF8F2D2E4AC3073EA4DD78C59080608549DA334C3EBB41F237D1796F3D7F1AF63047B137AB4CB789A7C28D66622E1B9419550F.GeneratedTextTransformation.Generator.Init(DatabaseReader databaseReader, String singleDbContextSubNamespace) in E:\DevRoot\Trash\EntityFramework-Reverse-POCO-Code-First-Generator\Tester.Integration.EfCore3\..\EntityFramework.Reverse.POCO.Generator\EF.Reverse.POCO.v3.ttinclude:line 4294
   at Microsoft.VisualStudio.TextTemplating737FAC30C3264AD8F42A0C259EBF8F2D2E4AC3073EA4DD78C59080608549DA334C3EBB41F237D1796F3D7F1AF63047B137AB4CB789A7C28D66622E1B9419550F.GeneratedTextTransformation.GeneratorFactory.Create(FileManagementService fileManagementService, Type fileManagerType, String singleDbContextSubNamespace) in E:\DevRoot\Trash\EntityFramework-Reverse-POCO-Code-First-Generator\Tester.Integration.EfCore3\..\EntityFramework.Reverse.POCO.Generator\EF.Reverse.POCO.v3.ttinclude:line 6529*/
// ------------------------------------------------------------------------------------------------


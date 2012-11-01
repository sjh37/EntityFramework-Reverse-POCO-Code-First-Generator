
// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `MyDbContext`
//     Connection String:      `Data Source=(local);Initial Catalog=aspnetdb;Integrated Security=True;Application Name=EntityFramework Reverse POCO Generator`

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.IO;
using System.Linq;
using System.Reflection;

namespace todo.Model
{
    public class MyDbContext : DbContext
    {
        public IDbSet<AspnetApplications> AspnetApplications { get; set; } // aspnet_Applications
        public IDbSet<AspnetMembership> AspnetMembership { get; set; } // aspnet_Membership
        public IDbSet<AspnetPaths> AspnetPaths { get; set; } // aspnet_Paths
        public IDbSet<AspnetPersonalizationAllUsers> AspnetPersonalizationAllUsers { get; set; } // aspnet_PersonalizationAllUsers
        public IDbSet<AspnetPersonalizationPerUser> AspnetPersonalizationPerUser { get; set; } // aspnet_PersonalizationPerUser
        public IDbSet<AspnetProfile> AspnetProfile { get; set; } // aspnet_Profile
        public IDbSet<AspnetRoles> AspnetRoles { get; set; } // aspnet_Roles
        public IDbSet<AspnetSchemaVersions> AspnetSchemaVersions { get; set; } // aspnet_SchemaVersions
        public IDbSet<AspnetUsers> AspnetUsers { get; set; } // aspnet_Users
        public IDbSet<AspnetUsersInRoles> AspnetUsersInRoles { get; set; } // aspnet_UsersInRoles
        public IDbSet<AspnetWebEventEvents> AspnetWebEventEvents { get; set; } // aspnet_WebEvent_Events
        public IDbSet<VwAspnetApplications> VwAspnetApplications { get; set; } // vw_aspnet_Applications
        public IDbSet<VwAspnetMembershipUsers> VwAspnetMembershipUsers { get; set; } // vw_aspnet_MembershipUsers
        public IDbSet<VwAspnetProfiles> VwAspnetProfiles { get; set; } // vw_aspnet_Profiles
        public IDbSet<VwAspnetRoles> VwAspnetRoles { get; set; } // vw_aspnet_Roles
        public IDbSet<VwAspnetUsers> VwAspnetUsers { get; set; } // vw_aspnet_Users
        public IDbSet<VwAspnetUsersInRoles> VwAspnetUsersInRoles { get; set; } // vw_aspnet_UsersInRoles
        public IDbSet<VwAspnetWebPartStatePaths> VwAspnetWebPartStatePaths { get; set; } // vw_aspnet_WebPartState_Paths
        public IDbSet<VwAspnetWebPartStateShared> VwAspnetWebPartStateShared { get; set; } // vw_aspnet_WebPartState_Shared
        public IDbSet<VwAspnetWebPartStateUser> VwAspnetWebPartStateUser { get; set; } // vw_aspnet_WebPartState_User

        static MyDbContext()
        {
            Database.SetInitializer<MyDbContext>(null);
        }

        public MyDbContext()
        {
        }

        public MyDbContext(string connectionString) : base(connectionString)
        {
        }

        private IEnumerable<Type> GetTypes()
        {
            var type = typeof(EntityTypeConfiguration<>);
            return GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(IDbSet<>))
                .Select(p => type.MakeGenericType(p.PropertyType.GetGenericArguments().First()))
                .ToArray();
        }

        private static void LoadAllEntityConfigurationsFromAllAssemblies(DbModelBuilder modelBuilder, IEnumerable<Type> types, string assemblyFilter, IEnumerable<string> namePartFilters)
        {
            var path = Path.GetDirectoryName(Path.GetFullPath(new Uri(Assembly.GetExecutingAssembly().EscapedCodeBase).LocalPath));
            if(string.IsNullOrEmpty(path))
                return;

            new DirectoryCatalog(path, assemblyFilter)
                .LoadedFiles
                .Where(x =>
                {
                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(x.ToLower());
                    if(fileNameWithoutExtension != null)
                    {
                        var parts = fileNameWithoutExtension.Split(".".ToCharArray());
                        return parts.Any(part => namePartFilters.Any(namePartFilter => part == namePartFilter.ToLower()));
                    }
                    return false;
                })
                .Select(Assembly.LoadFrom)
                .ToList()
                .ForEach(assembly => assembly.GetTypes()
                                         .Where(t => types.Contains(t.BaseType))
                                         .Select(Activator.CreateInstance)
                                         .ToList<dynamic>()
                                         .ForEach(instance => modelBuilder.Configurations.Add(instance)));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var typesToLoad = GetTypes();
            LoadAllEntityConfigurationsFromAllAssemblies(modelBuilder, typesToLoad, "*.dll", new[] { "Data", "Domain", "Poco", "Model" });
        }
    }

    // aspnet_Applications
    public class AspnetApplications
    {
        public virtual string ApplicationName { get; set; } // ApplicationName
        public virtual string LoweredApplicationName { get; set; } // LoweredApplicationName
        public virtual Guid ApplicationId { get; set; } // ApplicationId
        public virtual string Description { get; set; } // Description
    }

    // aspnet_Membership
    public class AspnetMembership
    {
        public virtual Guid ApplicationId { get; set; } // ApplicationId
        public virtual Guid UserId { get; set; } // UserId
        public virtual string Password { get; set; } // Password
        public virtual int PasswordFormat { get; set; } // PasswordFormat
        public virtual string PasswordSalt { get; set; } // PasswordSalt
        public virtual string MobilePin { get; set; } // MobilePIN
        public virtual string Email { get; set; } // Email
        public virtual string LoweredEmail { get; set; } // LoweredEmail
        public virtual string PasswordQuestion { get; set; } // PasswordQuestion
        public virtual string PasswordAnswer { get; set; } // PasswordAnswer
        public virtual bool IsApproved { get; set; } // IsApproved
        public virtual bool IsLockedOut { get; set; } // IsLockedOut
        public virtual DateTime CreateDate { get; set; } // CreateDate
        public virtual DateTime LastLoginDate { get; set; } // LastLoginDate
        public virtual DateTime LastPasswordChangedDate { get; set; } // LastPasswordChangedDate
        public virtual DateTime LastLockoutDate { get; set; } // LastLockoutDate
        public virtual int FailedPasswordAttemptCount { get; set; } // FailedPasswordAttemptCount
        public virtual DateTime FailedPasswordAttemptWindowStart { get; set; } // FailedPasswordAttemptWindowStart
        public virtual int FailedPasswordAnswerAttemptCount { get; set; } // FailedPasswordAnswerAttemptCount
        public virtual DateTime FailedPasswordAnswerAttemptWindowStart { get; set; } // FailedPasswordAnswerAttemptWindowStart
        public virtual string Comment { get; set; } // Comment

        // Foreign keys
        public virtual AspnetApplications ApplicationFk { get; set; } //  ApplicationId - FkAspnetMeAppli21B6055D
        public virtual AspnetUsers UserFk { get; set; } //  UserId - FkAspnetMeUserI22Aa2996
    }

    // aspnet_Paths
    public class AspnetPaths
    {
        public virtual Guid ApplicationId { get; set; } // ApplicationId
        public virtual Guid PathId { get; set; } // PathId
        public virtual string Path { get; set; } // Path
        public virtual string LoweredPath { get; set; } // LoweredPath

        // Foreign keys
        public virtual AspnetApplications ApplicationFk { get; set; } //  ApplicationId - FkAspnetPaAppli5Aee82B9
    }

    // aspnet_PersonalizationAllUsers
    public class AspnetPersonalizationAllUsers
    {
        public virtual Guid PathId { get; set; } // PathId
        public virtual byte[] PageSettings { get; set; } // PageSettings
        public virtual DateTime LastUpdatedDate { get; set; } // LastUpdatedDate

        // Foreign keys
        public virtual AspnetPaths PathFk { get; set; } //  PathId - FkAspnetPePathI628Fa481
    }

    // aspnet_PersonalizationPerUser
    public class AspnetPersonalizationPerUser
    {
        public virtual Guid Id { get; set; } // Id
        public virtual Guid? PathId { get; set; } // PathId
        public virtual Guid? UserId { get; set; } // UserId
        public virtual byte[] PageSettings { get; set; } // PageSettings
        public virtual DateTime LastUpdatedDate { get; set; } // LastUpdatedDate

        // Foreign keys
        public virtual AspnetPaths PathFk { get; set; } //  PathId - FkAspnetPePathI68487Dd7
        public virtual AspnetUsers UserFk { get; set; } //  UserId - FkAspnetPeUserI693Ca210
    }

    // aspnet_Profile
    public class AspnetProfile
    {
        public virtual Guid UserId { get; set; } // UserId
        public virtual string PropertyNames { get; set; } // PropertyNames
        public virtual string PropertyValuesString { get; set; } // PropertyValuesString
        public virtual byte[] PropertyValuesBinary { get; set; } // PropertyValuesBinary
        public virtual DateTime LastUpdatedDate { get; set; } // LastUpdatedDate

        // Foreign keys
        public virtual AspnetUsers UserFk { get; set; } //  UserId - FkAspnetPrUserI38996Ab5
    }

    // aspnet_Roles
    public class AspnetRoles
    {
        public virtual Guid ApplicationId { get; set; } // ApplicationId
        public virtual Guid RoleId { get; set; } // RoleId
        public virtual string RoleName { get; set; } // RoleName
        public virtual string LoweredRoleName { get; set; } // LoweredRoleName
        public virtual string Description { get; set; } // Description

        // Foreign keys
        public virtual AspnetApplications ApplicationFk { get; set; } //  ApplicationId - FkAspnetRoAppli440B1D61
    }

    // aspnet_SchemaVersions
    public class AspnetSchemaVersions
    {
        public virtual string Feature { get; set; } // Feature
        public virtual string CompatibleSchemaVersion { get; set; } // CompatibleSchemaVersion
        public virtual bool IsCurrentVersion { get; set; } // IsCurrentVersion
    }

    // aspnet_Users
    public class AspnetUsers
    {
        public virtual Guid ApplicationId { get; set; } // ApplicationId
        public virtual Guid UserId { get; set; } // UserId
        public virtual string UserName { get; set; } // UserName
        public virtual string LoweredUserName { get; set; } // LoweredUserName
        public virtual string MobileAlias { get; set; } // MobileAlias
        public virtual bool IsAnonymous { get; set; } // IsAnonymous
        public virtual DateTime LastActivityDate { get; set; } // LastActivityDate

        // Foreign keys
        public virtual AspnetApplications ApplicationFk { get; set; } //  ApplicationId - FkAspnetUsAppli0Daf0Cb0
    }

    // aspnet_UsersInRoles
    public class AspnetUsersInRoles
    {
        public virtual Guid UserId { get; set; } // UserId
        public virtual Guid RoleId { get; set; } // RoleId

        // Foreign keys
        public virtual AspnetUsers UserFk { get; set; } //  UserId - FkAspnetUsUserI49C3F6B7
        public virtual AspnetRoles RoleFk { get; set; } //  RoleId - FkAspnetUsRoleI4Ab81Af0
    }

    // aspnet_WebEvent_Events
    public class AspnetWebEventEvents
    {
        public virtual string EventId { get; set; } // EventId
        public virtual DateTime EventTimeUtc { get; set; } // EventTimeUtc
        public virtual DateTime EventTime { get; set; } // EventTime
        public virtual string EventType { get; set; } // EventType
        public virtual decimal EventSequence { get; set; } // EventSequence
        public virtual decimal EventOccurrence { get; set; } // EventOccurrence
        public virtual int EventCode { get; set; } // EventCode
        public virtual int EventDetailCode { get; set; } // EventDetailCode
        public virtual string Message { get; set; } // Message
        public virtual string ApplicationPath { get; set; } // ApplicationPath
        public virtual string ApplicationVirtualPath { get; set; } // ApplicationVirtualPath
        public virtual string MachineName { get; set; } // MachineName
        public virtual string RequestUrl { get; set; } // RequestUrl
        public virtual string ExceptionType { get; set; } // ExceptionType
        public virtual string Details { get; set; } // Details
    }

    // vw_aspnet_Applications
    public class VwAspnetApplications
    {
        public virtual string ApplicationName { get; set; } // ApplicationName
        public virtual string LoweredApplicationName { get; set; } // LoweredApplicationName
        public virtual Guid ApplicationId { get; set; } // ApplicationId
        public virtual string Description { get; set; } // Description
    }

    // vw_aspnet_MembershipUsers
    public class VwAspnetMembershipUsers
    {
        public virtual Guid UserId { get; set; } // UserId
        public virtual int PasswordFormat { get; set; } // PasswordFormat
        public virtual string MobilePin { get; set; } // MobilePIN
        public virtual string Email { get; set; } // Email
        public virtual string LoweredEmail { get; set; } // LoweredEmail
        public virtual string PasswordQuestion { get; set; } // PasswordQuestion
        public virtual string PasswordAnswer { get; set; } // PasswordAnswer
        public virtual bool IsApproved { get; set; } // IsApproved
        public virtual bool IsLockedOut { get; set; } // IsLockedOut
        public virtual DateTime CreateDate { get; set; } // CreateDate
        public virtual DateTime LastLoginDate { get; set; } // LastLoginDate
        public virtual DateTime LastPasswordChangedDate { get; set; } // LastPasswordChangedDate
        public virtual DateTime LastLockoutDate { get; set; } // LastLockoutDate
        public virtual int FailedPasswordAttemptCount { get; set; } // FailedPasswordAttemptCount
        public virtual DateTime FailedPasswordAttemptWindowStart { get; set; } // FailedPasswordAttemptWindowStart
        public virtual int FailedPasswordAnswerAttemptCount { get; set; } // FailedPasswordAnswerAttemptCount
        public virtual DateTime FailedPasswordAnswerAttemptWindowStart { get; set; } // FailedPasswordAnswerAttemptWindowStart
        public virtual string Comment { get; set; } // Comment
        public virtual Guid ApplicationId { get; set; } // ApplicationId
        public virtual string UserName { get; set; } // UserName
        public virtual string MobileAlias { get; set; } // MobileAlias
        public virtual bool IsAnonymous { get; set; } // IsAnonymous
        public virtual DateTime LastActivityDate { get; set; } // LastActivityDate
    }

    // vw_aspnet_Profiles
    public class VwAspnetProfiles
    {
        public virtual Guid UserId { get; set; } // UserId
        public virtual DateTime LastUpdatedDate { get; set; } // LastUpdatedDate
        public virtual int? DataSize { get; set; } // DataSize
    }

    // vw_aspnet_Roles
    public class VwAspnetRoles
    {
        public virtual Guid ApplicationId { get; set; } // ApplicationId
        public virtual Guid RoleId { get; set; } // RoleId
        public virtual string RoleName { get; set; } // RoleName
        public virtual string LoweredRoleName { get; set; } // LoweredRoleName
        public virtual string Description { get; set; } // Description
    }

    // vw_aspnet_Users
    public class VwAspnetUsers
    {
        public virtual Guid ApplicationId { get; set; } // ApplicationId
        public virtual Guid UserId { get; set; } // UserId
        public virtual string UserName { get; set; } // UserName
        public virtual string LoweredUserName { get; set; } // LoweredUserName
        public virtual string MobileAlias { get; set; } // MobileAlias
        public virtual bool IsAnonymous { get; set; } // IsAnonymous
        public virtual DateTime LastActivityDate { get; set; } // LastActivityDate
    }

    // vw_aspnet_UsersInRoles
    public class VwAspnetUsersInRoles
    {
        public virtual Guid UserId { get; set; } // UserId
        public virtual Guid RoleId { get; set; } // RoleId
    }

    // vw_aspnet_WebPartState_Paths
    public class VwAspnetWebPartStatePaths
    {
        public virtual Guid ApplicationId { get; set; } // ApplicationId
        public virtual Guid PathId { get; set; } // PathId
        public virtual string Path { get; set; } // Path
        public virtual string LoweredPath { get; set; } // LoweredPath
    }

    // vw_aspnet_WebPartState_Shared
    public class VwAspnetWebPartStateShared
    {
        public virtual Guid PathId { get; set; } // PathId
        public virtual int? DataSize { get; set; } // DataSize
        public virtual DateTime LastUpdatedDate { get; set; } // LastUpdatedDate
    }

    // vw_aspnet_WebPartState_User
    public class VwAspnetWebPartStateUser
    {
        public virtual Guid? PathId { get; set; } // PathId
        public virtual Guid? UserId { get; set; } // UserId
        public virtual int? DataSize { get; set; } // DataSize
        public virtual DateTime LastUpdatedDate { get; set; } // LastUpdatedDate
    }

}

namespace todo.ModelConfiguration
{
    using Model;

    // aspnet_Applications
    public class AspnetApplicationsConfiguration : EntityTypeConfiguration<AspnetApplications>
    {
        public AspnetApplicationsConfiguration()
        {
            ToTable("dbo.aspnet_Applications");
            HasKey(x => x.ApplicationId);

            Property(x => x.ApplicationName).HasColumnName("ApplicationName").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredApplicationName).HasColumnName("LoweredApplicationName").IsRequired().HasMaxLength(256);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
        }
    }

    // aspnet_Membership
    public class AspnetMembershipConfiguration : EntityTypeConfiguration<AspnetMembership>
    {
        public AspnetMembershipConfiguration()
        {
            ToTable("dbo.aspnet_Membership");
            HasKey(x => x.UserId);

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.Password).HasColumnName("Password").IsRequired().HasMaxLength(128);
            Property(x => x.PasswordFormat).HasColumnName("PasswordFormat").IsRequired();
            Property(x => x.PasswordSalt).HasColumnName("PasswordSalt").IsRequired().HasMaxLength(128);
            Property(x => x.MobilePin).HasColumnName("MobilePIN").IsOptional().HasMaxLength(16);
            Property(x => x.Email).HasColumnName("Email").IsOptional().HasMaxLength(256);
            Property(x => x.LoweredEmail).HasColumnName("LoweredEmail").IsOptional().HasMaxLength(256);
            Property(x => x.PasswordQuestion).HasColumnName("PasswordQuestion").IsOptional().HasMaxLength(256);
            Property(x => x.PasswordAnswer).HasColumnName("PasswordAnswer").IsOptional().HasMaxLength(128);
            Property(x => x.IsApproved).HasColumnName("IsApproved").IsRequired();
            Property(x => x.IsLockedOut).HasColumnName("IsLockedOut").IsRequired();
            Property(x => x.CreateDate).HasColumnName("CreateDate").IsRequired();
            Property(x => x.LastLoginDate).HasColumnName("LastLoginDate").IsRequired();
            Property(x => x.LastPasswordChangedDate).HasColumnName("LastPasswordChangedDate").IsRequired();
            Property(x => x.LastLockoutDate).HasColumnName("LastLockoutDate").IsRequired();
            Property(x => x.FailedPasswordAttemptCount).HasColumnName("FailedPasswordAttemptCount").IsRequired();
            Property(x => x.FailedPasswordAttemptWindowStart).HasColumnName("FailedPasswordAttemptWindowStart").IsRequired();
            Property(x => x.FailedPasswordAnswerAttemptCount).HasColumnName("FailedPasswordAnswerAttemptCount").IsRequired();
            Property(x => x.FailedPasswordAnswerAttemptWindowStart).HasColumnName("FailedPasswordAnswerAttemptWindowStart").IsRequired();
            Property(x => x.Comment).HasColumnName("Comment").IsOptional().HasMaxLength(1073741823);

            // Foreign keys
            HasRequired(a => a.ApplicationFk).WithMany().HasForeignKey(b => b.ApplicationId); // FkAspnetMeAppli21B6055D
            HasRequired(a => a.UserFk).WithMany().HasForeignKey(b => b.UserId); // FkAspnetMeUserI22Aa2996
        }
    }

    // aspnet_Paths
    public class AspnetPathsConfiguration : EntityTypeConfiguration<AspnetPaths>
    {
        public AspnetPathsConfiguration()
        {
            ToTable("dbo.aspnet_Paths");
            HasKey(x => x.PathId);

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.PathId).HasColumnName("PathId").IsRequired();
            Property(x => x.Path).HasColumnName("Path").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredPath).HasColumnName("LoweredPath").IsRequired().HasMaxLength(256);

            // Foreign keys
            HasRequired(a => a.ApplicationFk).WithMany().HasForeignKey(b => b.ApplicationId); // FkAspnetPaAppli5Aee82B9
        }
    }

    // aspnet_PersonalizationAllUsers
    public class AspnetPersonalizationAllUsersConfiguration : EntityTypeConfiguration<AspnetPersonalizationAllUsers>
    {
        public AspnetPersonalizationAllUsersConfiguration()
        {
            ToTable("dbo.aspnet_PersonalizationAllUsers");
            HasKey(x => x.PathId);

            Property(x => x.PathId).HasColumnName("PathId").IsRequired();
            Property(x => x.PageSettings).HasColumnName("PageSettings").IsRequired().HasMaxLength(2147483647);
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();

            // Foreign keys
            HasRequired(a => a.PathFk).WithMany().HasForeignKey(b => b.PathId); // FkAspnetPePathI628Fa481
        }
    }

    // aspnet_PersonalizationPerUser
    public class AspnetPersonalizationPerUserConfiguration : EntityTypeConfiguration<AspnetPersonalizationPerUser>
    {
        public AspnetPersonalizationPerUserConfiguration()
        {
            ToTable("dbo.aspnet_PersonalizationPerUser");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.PathId).HasColumnName("PathId").IsOptional();
            Property(x => x.UserId).HasColumnName("UserId").IsOptional();
            Property(x => x.PageSettings).HasColumnName("PageSettings").IsRequired().HasMaxLength(2147483647);
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();

            // Foreign keys
            HasOptional(a => a.PathFk).WithMany().HasForeignKey(b => b.PathId); // FkAspnetPePathI68487Dd7
            HasOptional(a => a.UserFk).WithMany().HasForeignKey(b => b.UserId); // FkAspnetPeUserI693Ca210
        }
    }

    // aspnet_Profile
    public class AspnetProfileConfiguration : EntityTypeConfiguration<AspnetProfile>
    {
        public AspnetProfileConfiguration()
        {
            ToTable("dbo.aspnet_Profile");
            HasKey(x => x.UserId);

            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.PropertyNames).HasColumnName("PropertyNames").IsRequired().HasMaxLength(1073741823);
            Property(x => x.PropertyValuesString).HasColumnName("PropertyValuesString").IsRequired().HasMaxLength(1073741823);
            Property(x => x.PropertyValuesBinary).HasColumnName("PropertyValuesBinary").IsRequired().HasMaxLength(2147483647);
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();

            // Foreign keys
            HasRequired(a => a.UserFk).WithMany().HasForeignKey(b => b.UserId); // FkAspnetPrUserI38996Ab5
        }
    }

    // aspnet_Roles
    public class AspnetRolesConfiguration : EntityTypeConfiguration<AspnetRoles>
    {
        public AspnetRolesConfiguration()
        {
            ToTable("dbo.aspnet_Roles");
            HasKey(x => x.RoleId);

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired();
            Property(x => x.RoleName).HasColumnName("RoleName").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredRoleName).HasColumnName("LoweredRoleName").IsRequired().HasMaxLength(256);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);

            // Foreign keys
            HasRequired(a => a.ApplicationFk).WithMany().HasForeignKey(b => b.ApplicationId); // FkAspnetRoAppli440B1D61
        }
    }

    // aspnet_SchemaVersions
    public class AspnetSchemaVersionsConfiguration : EntityTypeConfiguration<AspnetSchemaVersions>
    {
        public AspnetSchemaVersionsConfiguration()
        {
            ToTable("dbo.aspnet_SchemaVersions");
            HasKey(x => new { x.Feature, x.CompatibleSchemaVersion });

            Property(x => x.Feature).HasColumnName("Feature").IsRequired().HasMaxLength(128);
            Property(x => x.CompatibleSchemaVersion).HasColumnName("CompatibleSchemaVersion").IsRequired().HasMaxLength(128);
            Property(x => x.IsCurrentVersion).HasColumnName("IsCurrentVersion").IsRequired();
        }
    }

    // aspnet_Users
    public class AspnetUsersConfiguration : EntityTypeConfiguration<AspnetUsers>
    {
        public AspnetUsersConfiguration()
        {
            ToTable("dbo.aspnet_Users");
            HasKey(x => x.UserId);

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.UserName).HasColumnName("UserName").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredUserName).HasColumnName("LoweredUserName").IsRequired().HasMaxLength(256);
            Property(x => x.MobileAlias).HasColumnName("MobileAlias").IsOptional().HasMaxLength(16);
            Property(x => x.IsAnonymous).HasColumnName("IsAnonymous").IsRequired();
            Property(x => x.LastActivityDate).HasColumnName("LastActivityDate").IsRequired();

            // Foreign keys
            HasRequired(a => a.ApplicationFk).WithMany().HasForeignKey(b => b.ApplicationId); // FkAspnetUsAppli0Daf0Cb0
        }
    }

    // aspnet_UsersInRoles
    public class AspnetUsersInRolesConfiguration : EntityTypeConfiguration<AspnetUsersInRoles>
    {
        public AspnetUsersInRolesConfiguration()
        {
            ToTable("dbo.aspnet_UsersInRoles");
            HasKey(x => new { x.UserId, x.RoleId });

            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired();

            // Foreign keys
            HasRequired(a => a.UserFk).WithMany().HasForeignKey(b => b.UserId); // FkAspnetUsUserI49C3F6B7
            HasRequired(a => a.RoleFk).WithMany().HasForeignKey(b => b.RoleId); // FkAspnetUsRoleI4Ab81Af0
        }
    }

    // aspnet_WebEvent_Events
    public class AspnetWebEventEventsConfiguration : EntityTypeConfiguration<AspnetWebEventEvents>
    {
        public AspnetWebEventEventsConfiguration()
        {
            ToTable("dbo.aspnet_WebEvent_Events");
            HasKey(x => x.EventId);

            Property(x => x.EventId).HasColumnName("EventId").IsRequired().HasMaxLength(32);
            Property(x => x.EventTimeUtc).HasColumnName("EventTimeUtc").IsRequired();
            Property(x => x.EventTime).HasColumnName("EventTime").IsRequired();
            Property(x => x.EventType).HasColumnName("EventType").IsRequired().HasMaxLength(256);
            Property(x => x.EventSequence).HasColumnName("EventSequence").IsRequired();
            Property(x => x.EventOccurrence).HasColumnName("EventOccurrence").IsRequired();
            Property(x => x.EventCode).HasColumnName("EventCode").IsRequired();
            Property(x => x.EventDetailCode).HasColumnName("EventDetailCode").IsRequired();
            Property(x => x.Message).HasColumnName("Message").IsOptional().HasMaxLength(1024);
            Property(x => x.ApplicationPath).HasColumnName("ApplicationPath").IsOptional().HasMaxLength(256);
            Property(x => x.ApplicationVirtualPath).HasColumnName("ApplicationVirtualPath").IsOptional().HasMaxLength(256);
            Property(x => x.MachineName).HasColumnName("MachineName").IsRequired().HasMaxLength(256);
            Property(x => x.RequestUrl).HasColumnName("RequestUrl").IsOptional().HasMaxLength(1024);
            Property(x => x.ExceptionType).HasColumnName("ExceptionType").IsOptional().HasMaxLength(256);
            Property(x => x.Details).HasColumnName("Details").IsOptional().HasMaxLength(1073741823);
        }
    }

    // vw_aspnet_Applications
    public class VwAspnetApplicationsConfiguration : EntityTypeConfiguration<VwAspnetApplications>
    {
        public VwAspnetApplicationsConfiguration()
        {
            ToTable("dbo.vw_aspnet_Applications");

            Property(x => x.ApplicationName).HasColumnName("ApplicationName").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredApplicationName).HasColumnName("LoweredApplicationName").IsRequired().HasMaxLength(256);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
        }
    }

    // vw_aspnet_MembershipUsers
    public class VwAspnetMembershipUsersConfiguration : EntityTypeConfiguration<VwAspnetMembershipUsers>
    {
        public VwAspnetMembershipUsersConfiguration()
        {
            ToTable("dbo.vw_aspnet_MembershipUsers");

            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.PasswordFormat).HasColumnName("PasswordFormat").IsRequired();
            Property(x => x.MobilePin).HasColumnName("MobilePIN").IsOptional().HasMaxLength(16);
            Property(x => x.Email).HasColumnName("Email").IsOptional().HasMaxLength(256);
            Property(x => x.LoweredEmail).HasColumnName("LoweredEmail").IsOptional().HasMaxLength(256);
            Property(x => x.PasswordQuestion).HasColumnName("PasswordQuestion").IsOptional().HasMaxLength(256);
            Property(x => x.PasswordAnswer).HasColumnName("PasswordAnswer").IsOptional().HasMaxLength(128);
            Property(x => x.IsApproved).HasColumnName("IsApproved").IsRequired();
            Property(x => x.IsLockedOut).HasColumnName("IsLockedOut").IsRequired();
            Property(x => x.CreateDate).HasColumnName("CreateDate").IsRequired();
            Property(x => x.LastLoginDate).HasColumnName("LastLoginDate").IsRequired();
            Property(x => x.LastPasswordChangedDate).HasColumnName("LastPasswordChangedDate").IsRequired();
            Property(x => x.LastLockoutDate).HasColumnName("LastLockoutDate").IsRequired();
            Property(x => x.FailedPasswordAttemptCount).HasColumnName("FailedPasswordAttemptCount").IsRequired();
            Property(x => x.FailedPasswordAttemptWindowStart).HasColumnName("FailedPasswordAttemptWindowStart").IsRequired();
            Property(x => x.FailedPasswordAnswerAttemptCount).HasColumnName("FailedPasswordAnswerAttemptCount").IsRequired();
            Property(x => x.FailedPasswordAnswerAttemptWindowStart).HasColumnName("FailedPasswordAnswerAttemptWindowStart").IsRequired();
            Property(x => x.Comment).HasColumnName("Comment").IsOptional().HasMaxLength(1073741823);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.UserName).HasColumnName("UserName").IsRequired().HasMaxLength(256);
            Property(x => x.MobileAlias).HasColumnName("MobileAlias").IsOptional().HasMaxLength(16);
            Property(x => x.IsAnonymous).HasColumnName("IsAnonymous").IsRequired();
            Property(x => x.LastActivityDate).HasColumnName("LastActivityDate").IsRequired();
        }
    }

    // vw_aspnet_Profiles
    public class VwAspnetProfilesConfiguration : EntityTypeConfiguration<VwAspnetProfiles>
    {
        public VwAspnetProfilesConfiguration()
        {
            ToTable("dbo.vw_aspnet_Profiles");

            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();
            Property(x => x.DataSize).HasColumnName("DataSize").IsOptional();
        }
    }

    // vw_aspnet_Roles
    public class VwAspnetRolesConfiguration : EntityTypeConfiguration<VwAspnetRoles>
    {
        public VwAspnetRolesConfiguration()
        {
            ToTable("dbo.vw_aspnet_Roles");

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired();
            Property(x => x.RoleName).HasColumnName("RoleName").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredRoleName).HasColumnName("LoweredRoleName").IsRequired().HasMaxLength(256);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
        }
    }

    // vw_aspnet_Users
    public class VwAspnetUsersConfiguration : EntityTypeConfiguration<VwAspnetUsers>
    {
        public VwAspnetUsersConfiguration()
        {
            ToTable("dbo.vw_aspnet_Users");

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.UserName).HasColumnName("UserName").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredUserName).HasColumnName("LoweredUserName").IsRequired().HasMaxLength(256);
            Property(x => x.MobileAlias).HasColumnName("MobileAlias").IsOptional().HasMaxLength(16);
            Property(x => x.IsAnonymous).HasColumnName("IsAnonymous").IsRequired();
            Property(x => x.LastActivityDate).HasColumnName("LastActivityDate").IsRequired();
        }
    }

    // vw_aspnet_UsersInRoles
    public class VwAspnetUsersInRolesConfiguration : EntityTypeConfiguration<VwAspnetUsersInRoles>
    {
        public VwAspnetUsersInRolesConfiguration()
        {
            ToTable("dbo.vw_aspnet_UsersInRoles");

            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired();
        }
    }

    // vw_aspnet_WebPartState_Paths
    public class VwAspnetWebPartStatePathsConfiguration : EntityTypeConfiguration<VwAspnetWebPartStatePaths>
    {
        public VwAspnetWebPartStatePathsConfiguration()
        {
            ToTable("dbo.vw_aspnet_WebPartState_Paths");

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.PathId).HasColumnName("PathId").IsRequired();
            Property(x => x.Path).HasColumnName("Path").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredPath).HasColumnName("LoweredPath").IsRequired().HasMaxLength(256);
        }
    }

    // vw_aspnet_WebPartState_Shared
    public class VwAspnetWebPartStateSharedConfiguration : EntityTypeConfiguration<VwAspnetWebPartStateShared>
    {
        public VwAspnetWebPartStateSharedConfiguration()
        {
            ToTable("dbo.vw_aspnet_WebPartState_Shared");

            Property(x => x.PathId).HasColumnName("PathId").IsRequired();
            Property(x => x.DataSize).HasColumnName("DataSize").IsOptional();
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();
        }
    }

    // vw_aspnet_WebPartState_User
    public class VwAspnetWebPartStateUserConfiguration : EntityTypeConfiguration<VwAspnetWebPartStateUser>
    {
        public VwAspnetWebPartStateUserConfiguration()
        {
            ToTable("dbo.vw_aspnet_WebPartState_User");

            Property(x => x.PathId).HasColumnName("PathId").IsOptional();
            Property(x => x.UserId).HasColumnName("UserId").IsOptional();
            Property(x => x.DataSize).HasColumnName("DataSize").IsOptional();
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();
        }
    }

}


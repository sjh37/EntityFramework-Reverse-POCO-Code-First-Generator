

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Configuration file:     "EntityFramework.Reverse.POCO.Generator\App.config"
//     Connection String Name: "MyDbContext"
//     Connection String:      "Data Source=(local);Initial Catalog=aspnetdb;Integrated Security=True;Application Name=EntityFramework Reverse POCO Generator"

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace EntityFramework_Reverse_POCO_Generator
{
    // ************************************************************************
    // Unit of work
    public interface IMyDbContext : IDisposable
    {
        IDbSet<AspnetApplications> AspnetApplications { get; set; } // aspnet_Applications
        IDbSet<AspnetMembership> AspnetMembership { get; set; } // aspnet_Membership
        IDbSet<AspnetPaths> AspnetPaths { get; set; } // aspnet_Paths
        IDbSet<AspnetPersonalizationAllUsers> AspnetPersonalizationAllUsers { get; set; } // aspnet_PersonalizationAllUsers
        IDbSet<AspnetPersonalizationPerUser> AspnetPersonalizationPerUser { get; set; } // aspnet_PersonalizationPerUser
        IDbSet<AspnetProfile> AspnetProfile { get; set; } // aspnet_Profile
        IDbSet<AspnetRoles> AspnetRoles { get; set; } // aspnet_Roles
        IDbSet<AspnetSchemaVersions> AspnetSchemaVersions { get; set; } // aspnet_SchemaVersions
        IDbSet<AspnetUsers> AspnetUsers { get; set; } // aspnet_Users
        IDbSet<AspnetWebEventEvents> AspnetWebEventEvents { get; set; } // aspnet_WebEvent_Events
        IDbSet<VwAspnetApplications> VwAspnetApplications { get; set; } // vw_aspnet_Applications
        IDbSet<VwAspnetMembershipUsers> VwAspnetMembershipUsers { get; set; } // vw_aspnet_MembershipUsers
        IDbSet<VwAspnetProfiles> VwAspnetProfiles { get; set; } // vw_aspnet_Profiles
        IDbSet<VwAspnetRoles> VwAspnetRoles { get; set; } // vw_aspnet_Roles
        IDbSet<VwAspnetUsers> VwAspnetUsers { get; set; } // vw_aspnet_Users
        IDbSet<VwAspnetUsersInRoles> VwAspnetUsersInRoles { get; set; } // vw_aspnet_UsersInRoles
        IDbSet<VwAspnetWebPartStatePaths> VwAspnetWebPartStatePaths { get; set; } // vw_aspnet_WebPartState_Paths
        IDbSet<VwAspnetWebPartStateShared> VwAspnetWebPartStateShared { get; set; } // vw_aspnet_WebPartState_Shared
        IDbSet<VwAspnetWebPartStateUser> VwAspnetWebPartStateUser { get; set; } // vw_aspnet_WebPartState_User

        int SaveChanges();
    }

    // ************************************************************************
    // Database context
    public class MyDbContext : DbContext, IMyDbContext
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
            : base("Name=MyDbContext")
        {
        }

        public MyDbContext(string connectionString) : base(connectionString)
        {
        }

        public MyDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AspnetApplicationsConfiguration());
            modelBuilder.Configurations.Add(new AspnetMembershipConfiguration());
            modelBuilder.Configurations.Add(new AspnetPathsConfiguration());
            modelBuilder.Configurations.Add(new AspnetPersonalizationAllUsersConfiguration());
            modelBuilder.Configurations.Add(new AspnetPersonalizationPerUserConfiguration());
            modelBuilder.Configurations.Add(new AspnetProfileConfiguration());
            modelBuilder.Configurations.Add(new AspnetRolesConfiguration());
            modelBuilder.Configurations.Add(new AspnetSchemaVersionsConfiguration());
            modelBuilder.Configurations.Add(new AspnetUsersConfiguration());
            modelBuilder.Configurations.Add(new AspnetWebEventEventsConfiguration());
            modelBuilder.Configurations.Add(new VwAspnetApplicationsConfiguration());
            modelBuilder.Configurations.Add(new VwAspnetMembershipUsersConfiguration());
            modelBuilder.Configurations.Add(new VwAspnetProfilesConfiguration());
            modelBuilder.Configurations.Add(new VwAspnetRolesConfiguration());
            modelBuilder.Configurations.Add(new VwAspnetUsersConfiguration());
            modelBuilder.Configurations.Add(new VwAspnetUsersInRolesConfiguration());
            modelBuilder.Configurations.Add(new VwAspnetWebPartStatePathsConfiguration());
            modelBuilder.Configurations.Add(new VwAspnetWebPartStateSharedConfiguration());
            modelBuilder.Configurations.Add(new VwAspnetWebPartStateUserConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new AspnetApplicationsConfiguration(schema));
            modelBuilder.Configurations.Add(new AspnetMembershipConfiguration(schema));
            modelBuilder.Configurations.Add(new AspnetPathsConfiguration(schema));
            modelBuilder.Configurations.Add(new AspnetPersonalizationAllUsersConfiguration(schema));
            modelBuilder.Configurations.Add(new AspnetPersonalizationPerUserConfiguration(schema));
            modelBuilder.Configurations.Add(new AspnetProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new AspnetRolesConfiguration(schema));
            modelBuilder.Configurations.Add(new AspnetSchemaVersionsConfiguration(schema));
            modelBuilder.Configurations.Add(new AspnetUsersConfiguration(schema));
            modelBuilder.Configurations.Add(new AspnetWebEventEventsConfiguration(schema));
            modelBuilder.Configurations.Add(new VwAspnetApplicationsConfiguration(schema));
            modelBuilder.Configurations.Add(new VwAspnetMembershipUsersConfiguration(schema));
            modelBuilder.Configurations.Add(new VwAspnetProfilesConfiguration(schema));
            modelBuilder.Configurations.Add(new VwAspnetRolesConfiguration(schema));
            modelBuilder.Configurations.Add(new VwAspnetUsersConfiguration(schema));
            modelBuilder.Configurations.Add(new VwAspnetUsersInRolesConfiguration(schema));
            modelBuilder.Configurations.Add(new VwAspnetWebPartStatePathsConfiguration(schema));
            modelBuilder.Configurations.Add(new VwAspnetWebPartStateSharedConfiguration(schema));
            modelBuilder.Configurations.Add(new VwAspnetWebPartStateUserConfiguration(schema));
            return modelBuilder;
        }
    }

    // ************************************************************************
    // POCO classes

    // aspnet_Applications
    public class AspnetApplications
    {
        public string ApplicationName { get; set; } // ApplicationName
        public string LoweredApplicationName { get; set; } // LoweredApplicationName
        public Guid ApplicationId { get; set; } // ApplicationId (Primary key)
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<AspnetMembership> AspnetMembership { get; set; } // aspnet_Membership.FK__aspnet_Me__Appli__21B6055D
        public virtual ICollection<AspnetPaths> AspnetPaths { get; set; } // aspnet_Paths.FK__aspnet_Pa__Appli__5AEE82B9
        public virtual ICollection<AspnetRoles> AspnetRoles { get; set; } // aspnet_Roles.FK__aspnet_Ro__Appli__440B1D61
        public virtual ICollection<AspnetUsers> AspnetUsers { get; set; } // aspnet_Users.FK__aspnet_Us__Appli__0DAF0CB0

        public AspnetApplications()
        {
            ApplicationId = Guid.NewGuid();
            AspnetMembership = new List<AspnetMembership>();
            AspnetPaths = new List<AspnetPaths>();
            AspnetRoles = new List<AspnetRoles>();
            AspnetUsers = new List<AspnetUsers>();
        }
    }

    // aspnet_Membership
    public class AspnetMembership
    {
        public Guid ApplicationId { get; set; } // ApplicationId
        public Guid UserId { get; set; } // UserId (Primary key)
        public string Password { get; set; } // Password
        public int PasswordFormat { get; set; } // PasswordFormat
        public string PasswordSalt { get; set; } // PasswordSalt
        public string MobilePin { get; set; } // MobilePIN
        public string Email { get; set; } // Email
        public string LoweredEmail { get; set; } // LoweredEmail
        public string PasswordQuestion { get; set; } // PasswordQuestion
        public string PasswordAnswer { get; set; } // PasswordAnswer
        public bool IsApproved { get; set; } // IsApproved
        public bool IsLockedOut { get; set; } // IsLockedOut
        public DateTime CreateDate { get; set; } // CreateDate
        public DateTime LastLoginDate { get; set; } // LastLoginDate
        public DateTime LastPasswordChangedDate { get; set; } // LastPasswordChangedDate
        public DateTime LastLockoutDate { get; set; } // LastLockoutDate
        public int FailedPasswordAttemptCount { get; set; } // FailedPasswordAttemptCount
        public DateTime FailedPasswordAttemptWindowStart { get; set; } // FailedPasswordAttemptWindowStart
        public int FailedPasswordAnswerAttemptCount { get; set; } // FailedPasswordAnswerAttemptCount
        public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; } // FailedPasswordAnswerAttemptWindowStart
        public string Comment { get; set; } // Comment

        // Foreign keys
        public virtual AspnetApplications AspnetApplications { get; set; } //  ApplicationId - FK__aspnet_Me__Appli__21B6055D
        public virtual AspnetUsers AspnetUsers { get; set; } //  UserId - FK__aspnet_Me__UserI__22AA2996

        public AspnetMembership()
        {
            PasswordFormat = 0;
        }
    }

    // aspnet_Paths
    public class AspnetPaths
    {
        public Guid ApplicationId { get; set; } // ApplicationId
        public Guid PathId { get; set; } // PathId (Primary key)
        public string Path { get; set; } // Path
        public string LoweredPath { get; set; } // LoweredPath

        // Reverse navigation
        public virtual AspnetPersonalizationAllUsers AspnetPersonalizationAllUsers { get; set; } // aspnet_PersonalizationAllUsers.FK__aspnet_Pe__PathI__628FA481
        public virtual ICollection<AspnetPersonalizationPerUser> AspnetPersonalizationPerUser { get; set; } // aspnet_PersonalizationPerUser.FK__aspnet_Pe__PathI__68487DD7

        // Foreign keys
        public virtual AspnetApplications AspnetApplications { get; set; } //  ApplicationId - FK__aspnet_Pa__Appli__5AEE82B9

        public AspnetPaths()
        {
            PathId = Guid.NewGuid();
            AspnetPersonalizationPerUser = new List<AspnetPersonalizationPerUser>();
        }
    }

    // aspnet_PersonalizationAllUsers
    public class AspnetPersonalizationAllUsers
    {
        public Guid PathId { get; set; } // PathId (Primary key)
        public byte[] PageSettings { get; set; } // PageSettings
        public DateTime LastUpdatedDate { get; set; } // LastUpdatedDate

        // Foreign keys
        public virtual AspnetPaths AspnetPaths { get; set; } //  PathId - FK__aspnet_Pe__PathI__628FA481
    }

    // aspnet_PersonalizationPerUser
    public class AspnetPersonalizationPerUser
    {
        public Guid Id { get; set; } // Id (Primary key)
        public Guid? PathId { get; set; } // PathId
        public Guid? UserId { get; set; } // UserId
        public byte[] PageSettings { get; set; } // PageSettings
        public DateTime LastUpdatedDate { get; set; } // LastUpdatedDate

        // Foreign keys
        public virtual AspnetPaths AspnetPaths { get; set; } //  PathId - FK__aspnet_Pe__PathI__68487DD7
        public virtual AspnetUsers AspnetUsers { get; set; } //  UserId - FK__aspnet_Pe__UserI__693CA210

        public AspnetPersonalizationPerUser()
        {
            Id = Guid.NewGuid();
        }
    }

    // aspnet_Profile
    public class AspnetProfile
    {
        public Guid UserId { get; set; } // UserId (Primary key)
        public string PropertyNames { get; set; } // PropertyNames
        public string PropertyValuesString { get; set; } // PropertyValuesString
        public byte[] PropertyValuesBinary { get; set; } // PropertyValuesBinary
        public DateTime LastUpdatedDate { get; set; } // LastUpdatedDate

        // Foreign keys
        public virtual AspnetUsers AspnetUsers { get; set; } //  UserId - FK__aspnet_Pr__UserI__38996AB5
    }

    // aspnet_Roles
    public class AspnetRoles
    {
        public Guid ApplicationId { get; set; } // ApplicationId
        public Guid RoleId { get; set; } // RoleId (Primary key)
        public string RoleName { get; set; } // RoleName
        public string LoweredRoleName { get; set; } // LoweredRoleName
        public string Description { get; set; } // Description

        // Reverse navigation
        public virtual ICollection<AspnetUsers> AspnetUsers { get; set; } // Many to many mapping

        // Foreign keys
        public virtual AspnetApplications AspnetApplications { get; set; } //  ApplicationId - FK__aspnet_Ro__Appli__440B1D61

        public AspnetRoles()
        {
            RoleId = Guid.NewGuid();
            AspnetUsers = new List<AspnetUsers>();
        }
    }

    // aspnet_SchemaVersions
    public class AspnetSchemaVersions
    {
        public string Feature { get; set; } // Feature (Primary key)
        public string CompatibleSchemaVersion { get; set; } // CompatibleSchemaVersion (Primary key)
        public bool IsCurrentVersion { get; set; } // IsCurrentVersion
    }

    // aspnet_Users
    public class AspnetUsers
    {
        public Guid ApplicationId { get; set; } // ApplicationId
        public Guid UserId { get; set; } // UserId (Primary key)
        public string UserName { get; set; } // UserName
        public string LoweredUserName { get; set; } // LoweredUserName
        public string MobileAlias { get; set; } // MobileAlias
        public bool IsAnonymous { get; set; } // IsAnonymous
        public DateTime LastActivityDate { get; set; } // LastActivityDate

        // Reverse navigation
        public virtual ICollection<AspnetRoles> AspnetRoles { get; set; } // Many to many mapping
        public virtual AspnetMembership AspnetMembership { get; set; } // aspnet_Membership.FK__aspnet_Me__UserI__22AA2996
        public virtual ICollection<AspnetPersonalizationPerUser> AspnetPersonalizationPerUser { get; set; } // aspnet_PersonalizationPerUser.FK__aspnet_Pe__UserI__693CA210
        public virtual AspnetProfile AspnetProfile { get; set; } // aspnet_Profile.FK__aspnet_Pr__UserI__38996AB5

        // Foreign keys
        public virtual AspnetApplications AspnetApplications { get; set; } //  ApplicationId - FK__aspnet_Us__Appli__0DAF0CB0

        public AspnetUsers()
        {
            UserId = Guid.NewGuid();
            MobileAlias = "NULL";
            IsAnonymous = false;
            AspnetRoles = new List<AspnetRoles>();
            AspnetPersonalizationPerUser = new List<AspnetPersonalizationPerUser>();
        }
    }

    // aspnet_WebEvent_Events
    public class AspnetWebEventEvents
    {
        public string EventId { get; set; } // EventId (Primary key)
        public DateTime EventTimeUtc { get; set; } // EventTimeUtc
        public DateTime EventTime { get; set; } // EventTime
        public string EventType { get; set; } // EventType
        public decimal EventSequence { get; set; } // EventSequence
        public decimal EventOccurrence { get; set; } // EventOccurrence
        public int EventCode { get; set; } // EventCode
        public int EventDetailCode { get; set; } // EventDetailCode
        public string Message { get; set; } // Message
        public string ApplicationPath { get; set; } // ApplicationPath
        public string ApplicationVirtualPath { get; set; } // ApplicationVirtualPath
        public string MachineName { get; set; } // MachineName
        public string RequestUrl { get; set; } // RequestUrl
        public string ExceptionType { get; set; } // ExceptionType
        public string Details { get; set; } // Details
    }

    // vw_aspnet_Applications
    public class VwAspnetApplications
    {
        public string ApplicationName { get; set; } // ApplicationName
        public string LoweredApplicationName { get; set; } // LoweredApplicationName
        public Guid ApplicationId { get; set; } // ApplicationId
        public string Description { get; set; } // Description
    }

    // vw_aspnet_MembershipUsers
    public class VwAspnetMembershipUsers
    {
        public Guid UserId { get; set; } // UserId
        public int PasswordFormat { get; set; } // PasswordFormat
        public string MobilePin { get; set; } // MobilePIN
        public string Email { get; set; } // Email
        public string LoweredEmail { get; set; } // LoweredEmail
        public string PasswordQuestion { get; set; } // PasswordQuestion
        public string PasswordAnswer { get; set; } // PasswordAnswer
        public bool IsApproved { get; set; } // IsApproved
        public bool IsLockedOut { get; set; } // IsLockedOut
        public DateTime CreateDate { get; set; } // CreateDate
        public DateTime LastLoginDate { get; set; } // LastLoginDate
        public DateTime LastPasswordChangedDate { get; set; } // LastPasswordChangedDate
        public DateTime LastLockoutDate { get; set; } // LastLockoutDate
        public int FailedPasswordAttemptCount { get; set; } // FailedPasswordAttemptCount
        public DateTime FailedPasswordAttemptWindowStart { get; set; } // FailedPasswordAttemptWindowStart
        public int FailedPasswordAnswerAttemptCount { get; set; } // FailedPasswordAnswerAttemptCount
        public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; } // FailedPasswordAnswerAttemptWindowStart
        public string Comment { get; set; } // Comment
        public Guid ApplicationId { get; set; } // ApplicationId
        public string UserName { get; set; } // UserName
        public string MobileAlias { get; set; } // MobileAlias
        public bool IsAnonymous { get; set; } // IsAnonymous
        public DateTime LastActivityDate { get; set; } // LastActivityDate
    }

    // vw_aspnet_Profiles
    public class VwAspnetProfiles
    {
        public Guid UserId { get; set; } // UserId
        public DateTime LastUpdatedDate { get; set; } // LastUpdatedDate
        public int? DataSize { get; set; } // DataSize
    }

    // vw_aspnet_Roles
    public class VwAspnetRoles
    {
        public Guid ApplicationId { get; set; } // ApplicationId
        public Guid RoleId { get; set; } // RoleId
        public string RoleName { get; set; } // RoleName
        public string LoweredRoleName { get; set; } // LoweredRoleName
        public string Description { get; set; } // Description
    }

    // vw_aspnet_Users
    public class VwAspnetUsers
    {
        public Guid ApplicationId { get; set; } // ApplicationId
        public Guid UserId { get; set; } // UserId
        public string UserName { get; set; } // UserName
        public string LoweredUserName { get; set; } // LoweredUserName
        public string MobileAlias { get; set; } // MobileAlias
        public bool IsAnonymous { get; set; } // IsAnonymous
        public DateTime LastActivityDate { get; set; } // LastActivityDate
    }

    // vw_aspnet_UsersInRoles
    public class VwAspnetUsersInRoles
    {
        public Guid UserId { get; set; } // UserId
        public Guid RoleId { get; set; } // RoleId
    }

    // vw_aspnet_WebPartState_Paths
    public class VwAspnetWebPartStatePaths
    {
        public Guid ApplicationId { get; set; } // ApplicationId
        public Guid PathId { get; set; } // PathId
        public string Path { get; set; } // Path
        public string LoweredPath { get; set; } // LoweredPath
    }

    // vw_aspnet_WebPartState_Shared
    public class VwAspnetWebPartStateShared
    {
        public Guid PathId { get; set; } // PathId
        public int? DataSize { get; set; } // DataSize
        public DateTime LastUpdatedDate { get; set; } // LastUpdatedDate
    }

    // vw_aspnet_WebPartState_User
    public class VwAspnetWebPartStateUser
    {
        public Guid? PathId { get; set; } // PathId
        public Guid? UserId { get; set; } // UserId
        public int? DataSize { get; set; } // DataSize
        public DateTime LastUpdatedDate { get; set; } // LastUpdatedDate
    }


    // ************************************************************************
    // POCO Configuration

    // aspnet_Applications
    internal class AspnetApplicationsConfiguration : EntityTypeConfiguration<AspnetApplications>
    {
        public AspnetApplicationsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".aspnet_Applications");
            HasKey(x => x.ApplicationId);

            Property(x => x.ApplicationName).HasColumnName("ApplicationName").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredApplicationName).HasColumnName("LoweredApplicationName").IsRequired().HasMaxLength(256);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
        }
    }

    // aspnet_Membership
    internal class AspnetMembershipConfiguration : EntityTypeConfiguration<AspnetMembership>
    {
        public AspnetMembershipConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".aspnet_Membership");
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
            HasRequired(a => a.AspnetApplications).WithMany(b => b.AspnetMembership).HasForeignKey(c => c.ApplicationId); // FK__aspnet_Me__Appli__21B6055D
            HasRequired(a => a.AspnetUsers).WithOptional(b => b.AspnetMembership); // FK__aspnet_Me__UserI__22AA2996
        }
    }

    // aspnet_Paths
    internal class AspnetPathsConfiguration : EntityTypeConfiguration<AspnetPaths>
    {
        public AspnetPathsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".aspnet_Paths");
            HasKey(x => x.PathId);

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.PathId).HasColumnName("PathId").IsRequired();
            Property(x => x.Path).HasColumnName("Path").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredPath).HasColumnName("LoweredPath").IsRequired().HasMaxLength(256);

            // Foreign keys
            HasRequired(a => a.AspnetApplications).WithMany(b => b.AspnetPaths).HasForeignKey(c => c.ApplicationId); // FK__aspnet_Pa__Appli__5AEE82B9
        }
    }

    // aspnet_PersonalizationAllUsers
    internal class AspnetPersonalizationAllUsersConfiguration : EntityTypeConfiguration<AspnetPersonalizationAllUsers>
    {
        public AspnetPersonalizationAllUsersConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".aspnet_PersonalizationAllUsers");
            HasKey(x => x.PathId);

            Property(x => x.PathId).HasColumnName("PathId").IsRequired();
            Property(x => x.PageSettings).HasColumnName("PageSettings").IsRequired().HasMaxLength(2147483647);
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();

            // Foreign keys
            HasRequired(a => a.AspnetPaths).WithOptional(b => b.AspnetPersonalizationAllUsers); // FK__aspnet_Pe__PathI__628FA481
        }
    }

    // aspnet_PersonalizationPerUser
    internal class AspnetPersonalizationPerUserConfiguration : EntityTypeConfiguration<AspnetPersonalizationPerUser>
    {
        public AspnetPersonalizationPerUserConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".aspnet_PersonalizationPerUser");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired();
            Property(x => x.PathId).HasColumnName("PathId").IsOptional();
            Property(x => x.UserId).HasColumnName("UserId").IsOptional();
            Property(x => x.PageSettings).HasColumnName("PageSettings").IsRequired().HasMaxLength(2147483647);
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();

            // Foreign keys
            HasOptional(a => a.AspnetPaths).WithMany(b => b.AspnetPersonalizationPerUser).HasForeignKey(c => c.PathId); // FK__aspnet_Pe__PathI__68487DD7
            HasOptional(a => a.AspnetUsers).WithMany(b => b.AspnetPersonalizationPerUser).HasForeignKey(c => c.UserId); // FK__aspnet_Pe__UserI__693CA210
        }
    }

    // aspnet_Profile
    internal class AspnetProfileConfiguration : EntityTypeConfiguration<AspnetProfile>
    {
        public AspnetProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".aspnet_Profile");
            HasKey(x => x.UserId);

            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.PropertyNames).HasColumnName("PropertyNames").IsRequired().HasMaxLength(1073741823);
            Property(x => x.PropertyValuesString).HasColumnName("PropertyValuesString").IsRequired().HasMaxLength(1073741823);
            Property(x => x.PropertyValuesBinary).HasColumnName("PropertyValuesBinary").IsRequired().HasMaxLength(2147483647);
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();

            // Foreign keys
            HasRequired(a => a.AspnetUsers).WithOptional(b => b.AspnetProfile); // FK__aspnet_Pr__UserI__38996AB5
        }
    }

    // aspnet_Roles
    internal class AspnetRolesConfiguration : EntityTypeConfiguration<AspnetRoles>
    {
        public AspnetRolesConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".aspnet_Roles");
            HasKey(x => x.RoleId);

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired();
            Property(x => x.RoleName).HasColumnName("RoleName").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredRoleName).HasColumnName("LoweredRoleName").IsRequired().HasMaxLength(256);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);

            // Foreign keys
            HasRequired(a => a.AspnetApplications).WithMany(b => b.AspnetRoles).HasForeignKey(c => c.ApplicationId); // FK__aspnet_Ro__Appli__440B1D61
            HasMany(t => t.AspnetUsers).WithMany(t => t.AspnetRoles).Map(m => 
            {
                m.ToTable("aspnet_UsersInRoles");
                m.MapLeftKey("RoleId");
                m.MapRightKey("UserId");
            });
        }
    }

    // aspnet_SchemaVersions
    internal class AspnetSchemaVersionsConfiguration : EntityTypeConfiguration<AspnetSchemaVersions>
    {
        public AspnetSchemaVersionsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".aspnet_SchemaVersions");
            HasKey(x => new { x.Feature, x.CompatibleSchemaVersion });

            Property(x => x.Feature).HasColumnName("Feature").IsRequired().HasMaxLength(128);
            Property(x => x.CompatibleSchemaVersion).HasColumnName("CompatibleSchemaVersion").IsRequired().HasMaxLength(128);
            Property(x => x.IsCurrentVersion).HasColumnName("IsCurrentVersion").IsRequired();
        }
    }

    // aspnet_Users
    internal class AspnetUsersConfiguration : EntityTypeConfiguration<AspnetUsers>
    {
        public AspnetUsersConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".aspnet_Users");
            HasKey(x => x.UserId);

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.UserName).HasColumnName("UserName").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredUserName).HasColumnName("LoweredUserName").IsRequired().HasMaxLength(256);
            Property(x => x.MobileAlias).HasColumnName("MobileAlias").IsOptional().HasMaxLength(16);
            Property(x => x.IsAnonymous).HasColumnName("IsAnonymous").IsRequired();
            Property(x => x.LastActivityDate).HasColumnName("LastActivityDate").IsRequired();

            // Foreign keys
            HasRequired(a => a.AspnetApplications).WithMany(b => b.AspnetUsers).HasForeignKey(c => c.ApplicationId); // FK__aspnet_Us__Appli__0DAF0CB0
        }
    }

    // aspnet_WebEvent_Events
    internal class AspnetWebEventEventsConfiguration : EntityTypeConfiguration<AspnetWebEventEvents>
    {
        public AspnetWebEventEventsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".aspnet_WebEvent_Events");
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
    internal class VwAspnetApplicationsConfiguration : EntityTypeConfiguration<VwAspnetApplications>
    {
        public VwAspnetApplicationsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vw_aspnet_Applications");
            HasKey(x => new { x.ApplicationName, x.LoweredApplicationName, x.ApplicationId });

            Property(x => x.ApplicationName).HasColumnName("ApplicationName").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredApplicationName).HasColumnName("LoweredApplicationName").IsRequired().HasMaxLength(256);
            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
        }
    }

    // vw_aspnet_MembershipUsers
    internal class VwAspnetMembershipUsersConfiguration : EntityTypeConfiguration<VwAspnetMembershipUsers>
    {
        public VwAspnetMembershipUsersConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vw_aspnet_MembershipUsers");
            HasKey(x => new { x.UserId, x.PasswordFormat, x.IsApproved, x.IsLockedOut, x.CreateDate, x.LastLoginDate, x.LastPasswordChangedDate, x.LastLockoutDate, x.FailedPasswordAttemptCount, x.FailedPasswordAttemptWindowStart, x.FailedPasswordAnswerAttemptCount, x.FailedPasswordAnswerAttemptWindowStart, x.ApplicationId, x.UserName, x.IsAnonymous, x.LastActivityDate });

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
    internal class VwAspnetProfilesConfiguration : EntityTypeConfiguration<VwAspnetProfiles>
    {
        public VwAspnetProfilesConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vw_aspnet_Profiles");
            HasKey(x => new { x.UserId, x.LastUpdatedDate });

            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();
            Property(x => x.DataSize).HasColumnName("DataSize").IsOptional();
        }
    }

    // vw_aspnet_Roles
    internal class VwAspnetRolesConfiguration : EntityTypeConfiguration<VwAspnetRoles>
    {
        public VwAspnetRolesConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vw_aspnet_Roles");
            HasKey(x => new { x.ApplicationId, x.RoleId, x.RoleName, x.LoweredRoleName });

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired();
            Property(x => x.RoleName).HasColumnName("RoleName").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredRoleName).HasColumnName("LoweredRoleName").IsRequired().HasMaxLength(256);
            Property(x => x.Description).HasColumnName("Description").IsOptional().HasMaxLength(256);
        }
    }

    // vw_aspnet_Users
    internal class VwAspnetUsersConfiguration : EntityTypeConfiguration<VwAspnetUsers>
    {
        public VwAspnetUsersConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vw_aspnet_Users");
            HasKey(x => new { x.ApplicationId, x.UserId, x.UserName, x.LoweredUserName, x.IsAnonymous, x.LastActivityDate });

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
    internal class VwAspnetUsersInRolesConfiguration : EntityTypeConfiguration<VwAspnetUsersInRoles>
    {
        public VwAspnetUsersInRolesConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vw_aspnet_UsersInRoles");
            HasKey(x => new { x.UserId, x.RoleId });

            Property(x => x.UserId).HasColumnName("UserId").IsRequired();
            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired();
        }
    }

    // vw_aspnet_WebPartState_Paths
    internal class VwAspnetWebPartStatePathsConfiguration : EntityTypeConfiguration<VwAspnetWebPartStatePaths>
    {
        public VwAspnetWebPartStatePathsConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vw_aspnet_WebPartState_Paths");
            HasKey(x => new { x.ApplicationId, x.PathId, x.Path, x.LoweredPath });

            Property(x => x.ApplicationId).HasColumnName("ApplicationId").IsRequired();
            Property(x => x.PathId).HasColumnName("PathId").IsRequired();
            Property(x => x.Path).HasColumnName("Path").IsRequired().HasMaxLength(256);
            Property(x => x.LoweredPath).HasColumnName("LoweredPath").IsRequired().HasMaxLength(256);
        }
    }

    // vw_aspnet_WebPartState_Shared
    internal class VwAspnetWebPartStateSharedConfiguration : EntityTypeConfiguration<VwAspnetWebPartStateShared>
    {
        public VwAspnetWebPartStateSharedConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vw_aspnet_WebPartState_Shared");
            HasKey(x => new { x.PathId, x.LastUpdatedDate });

            Property(x => x.PathId).HasColumnName("PathId").IsRequired();
            Property(x => x.DataSize).HasColumnName("DataSize").IsOptional();
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();
        }
    }

    // vw_aspnet_WebPartState_User
    internal class VwAspnetWebPartStateUserConfiguration : EntityTypeConfiguration<VwAspnetWebPartStateUser>
    {
        public VwAspnetWebPartStateUserConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".vw_aspnet_WebPartState_User");
            HasKey(x => x.LastUpdatedDate);

            Property(x => x.PathId).HasColumnName("PathId").IsOptional();
            Property(x => x.UserId).HasColumnName("UserId").IsOptional();
            Property(x => x.DataSize).HasColumnName("DataSize").IsOptional();
            Property(x => x.LastUpdatedDate).HasColumnName("LastUpdatedDate").IsRequired();
        }
    }

}


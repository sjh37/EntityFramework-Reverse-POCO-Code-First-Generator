

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "MyDbContext"
//     Connection String:      "Data Source=(local);Initial Catalog=aspnetdb;Integrated Security=True;Application Name=EntityFramework Reverse POCO Generator"

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

// ReSharper disable DoNotCallOverridableMethodsInConstructor

namespace EntityFramework_Reverse_POCO_Generator
{
    // ************************************************************************
    // Database context
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
            modelBuilder.Configurations.Add(new AspnetUsersInRolesConfiguration());
            modelBuilder.Configurations.Add(new AspnetWebEventEventsConfiguration());
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
        public virtual ICollection<AspnetMembership> AspnetMembership { get; set; } // aspnet_Membership.FK__aspnet_Me__Appli__21B6055D;
        public virtual ICollection<AspnetPaths> AspnetPaths { get; set; } // aspnet_Paths.FK__aspnet_Pa__Appli__5AEE82B9;
        public virtual ICollection<AspnetRoles> AspnetRoles { get; set; } // aspnet_Roles.FK__aspnet_Ro__Appli__440B1D61;
        public virtual ICollection<AspnetUsers> AspnetUsers { get; set; } // aspnet_Users.FK__aspnet_Us__Appli__0DAF0CB0;

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
        public virtual AspnetPersonalizationAllUsers AspnetPersonalizationAllUsers { get; set; } // aspnet_PersonalizationAllUsers.FK__aspnet_Pe__PathI__628FA481;
        public virtual ICollection<AspnetPersonalizationPerUser> AspnetPersonalizationPerUser { get; set; } // aspnet_PersonalizationPerUser.FK__aspnet_Pe__PathI__68487DD7;

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
        public virtual ICollection<AspnetUsersInRoles> AspnetUsersInRoles { get; set; } // aspnet_UsersInRoles.FK__aspnet_Us__RoleI__4AB81AF0;

        // Foreign keys
        public virtual AspnetApplications AspnetApplications { get; set; } //  ApplicationId - FK__aspnet_Ro__Appli__440B1D61

        public AspnetRoles()
        {
            RoleId = Guid.NewGuid();
            AspnetUsersInRoles = new List<AspnetUsersInRoles>();
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
        public virtual AspnetMembership AspnetMembership { get; set; } // aspnet_Membership.FK__aspnet_Me__UserI__22AA2996;
        public virtual ICollection<AspnetPersonalizationPerUser> AspnetPersonalizationPerUser { get; set; } // aspnet_PersonalizationPerUser.FK__aspnet_Pe__UserI__693CA210;
        public virtual AspnetProfile AspnetProfile { get; set; } // aspnet_Profile.FK__aspnet_Pr__UserI__38996AB5;
        public virtual ICollection<AspnetUsersInRoles> AspnetUsersInRoles { get; set; } // aspnet_UsersInRoles.FK__aspnet_Us__UserI__49C3F6B7;

        // Foreign keys
        public virtual AspnetApplications AspnetApplications { get; set; } //  ApplicationId - FK__aspnet_Us__Appli__0DAF0CB0

        public AspnetUsers()
        {
            UserId = Guid.NewGuid();
            MobileAlias = "NULL";
            IsAnonymous = false;
            AspnetPersonalizationPerUser = new List<AspnetPersonalizationPerUser>();
            AspnetUsersInRoles = new List<AspnetUsersInRoles>();
        }
    }

    // aspnet_UsersInRoles
    public class AspnetUsersInRoles
    {
        public Guid UserId { get; set; } // UserId (Primary key)
        public Guid RoleId { get; set; } // RoleId (Primary key)

        // Foreign keys
        public virtual AspnetUsers AspnetUsers { get; set; } //  UserId - FK__aspnet_Us__UserI__49C3F6B7
        public virtual AspnetRoles AspnetRoles { get; set; } //  RoleId - FK__aspnet_Us__RoleI__4AB81AF0
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


    // ************************************************************************
    // POCO Configuration

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
            HasRequired(a => a.AspnetApplications).WithMany(b => b.AspnetMembership).HasForeignKey(c => c.ApplicationId); // FK__aspnet_Me__Appli__21B6055D
            HasRequired(a => a.AspnetUsers).WithOptional(b => b.AspnetMembership); // FK__aspnet_Me__UserI__22AA2996
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
            HasRequired(a => a.AspnetApplications).WithMany(b => b.AspnetPaths).HasForeignKey(c => c.ApplicationId); // FK__aspnet_Pa__Appli__5AEE82B9
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
            HasRequired(a => a.AspnetPaths).WithOptional(b => b.AspnetPersonalizationAllUsers); // FK__aspnet_Pe__PathI__628FA481
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
            HasOptional(a => a.AspnetPaths).WithMany(b => b.AspnetPersonalizationPerUser).HasForeignKey(c => c.PathId); // FK__aspnet_Pe__PathI__68487DD7
            HasOptional(a => a.AspnetUsers).WithMany(b => b.AspnetPersonalizationPerUser).HasForeignKey(c => c.UserId); // FK__aspnet_Pe__UserI__693CA210
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
            HasRequired(a => a.AspnetUsers).WithOptional(b => b.AspnetProfile); // FK__aspnet_Pr__UserI__38996AB5
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
            HasRequired(a => a.AspnetApplications).WithMany(b => b.AspnetRoles).HasForeignKey(c => c.ApplicationId); // FK__aspnet_Ro__Appli__440B1D61
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
            HasRequired(a => a.AspnetApplications).WithMany(b => b.AspnetUsers).HasForeignKey(c => c.ApplicationId); // FK__aspnet_Us__Appli__0DAF0CB0
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
            HasRequired(a => a.AspnetUsers).WithMany(b => b.AspnetUsersInRoles).HasForeignKey(c => c.UserId); // FK__aspnet_Us__UserI__49C3F6B7
            HasRequired(a => a.AspnetRoles).WithMany(b => b.AspnetUsersInRoles).HasForeignKey(c => c.RoleId); // FK__aspnet_Us__RoleI__4AB81AF0
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

}


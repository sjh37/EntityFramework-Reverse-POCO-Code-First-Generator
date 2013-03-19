

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: "MyDbContext"
//     Connection String:      "Data Source=(local);Initial Catalog=aspnetdb;Integrated Security=True;Application Name=EntityFramework Reverse POCO Generator"

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

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
    [DataContract]
    public class AspnetApplications
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string ApplicationName { get; set; } // ApplicationName

        [DataMember(Order = 2, IsRequired = false)]
        public string LoweredApplicationName { get; set; } // LoweredApplicationName

        [DataMember(Order = 3, IsRequired = false)]
        public Guid ApplicationId { get; set; } // ApplicationId (Primary key)

        [DataMember(Order = 4, IsRequired = true)]
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
    [DataContract]
    public class AspnetMembership
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid ApplicationId { get; set; } // ApplicationId

        [DataMember(Order = 2, IsRequired = false)]
        public Guid UserId { get; set; } // UserId (Primary key)

        [DataMember(Order = 3, IsRequired = false)]
        public string Password { get; set; } // Password

        [DataMember(Order = 4, IsRequired = false)]
        public int PasswordFormat { get; set; } // PasswordFormat

        [DataMember(Order = 5, IsRequired = false)]
        public string PasswordSalt { get; set; } // PasswordSalt

        [DataMember(Order = 6, IsRequired = true)]
        public string MobilePin { get; set; } // MobilePIN

        [DataMember(Order = 7, IsRequired = true)]
        public string Email { get; set; } // Email

        [DataMember(Order = 8, IsRequired = true)]
        public string LoweredEmail { get; set; } // LoweredEmail

        [DataMember(Order = 9, IsRequired = true)]
        public string PasswordQuestion { get; set; } // PasswordQuestion

        [DataMember(Order = 10, IsRequired = true)]
        public string PasswordAnswer { get; set; } // PasswordAnswer

        [DataMember(Order = 11, IsRequired = false)]
        public bool IsApproved { get; set; } // IsApproved

        [DataMember(Order = 12, IsRequired = false)]
        public bool IsLockedOut { get; set; } // IsLockedOut

        [DataMember(Order = 13, IsRequired = false)]
        public DateTime CreateDate { get; set; } // CreateDate

        [DataMember(Order = 14, IsRequired = false)]
        public DateTime LastLoginDate { get; set; } // LastLoginDate

        [DataMember(Order = 15, IsRequired = false)]
        public DateTime LastPasswordChangedDate { get; set; } // LastPasswordChangedDate

        [DataMember(Order = 16, IsRequired = false)]
        public DateTime LastLockoutDate { get; set; } // LastLockoutDate

        [DataMember(Order = 17, IsRequired = false)]
        public int FailedPasswordAttemptCount { get; set; } // FailedPasswordAttemptCount

        [DataMember(Order = 18, IsRequired = false)]
        public DateTime FailedPasswordAttemptWindowStart { get; set; } // FailedPasswordAttemptWindowStart

        [DataMember(Order = 19, IsRequired = false)]
        public int FailedPasswordAnswerAttemptCount { get; set; } // FailedPasswordAnswerAttemptCount

        [DataMember(Order = 20, IsRequired = false)]
        public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; } // FailedPasswordAnswerAttemptWindowStart

        [DataMember(Order = 21, IsRequired = true)]
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
    [DataContract]
    public class AspnetPaths
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid ApplicationId { get; set; } // ApplicationId

        [DataMember(Order = 2, IsRequired = false)]
        public Guid PathId { get; set; } // PathId (Primary key)

        [DataMember(Order = 3, IsRequired = false)]
        public string Path { get; set; } // Path

        [DataMember(Order = 4, IsRequired = false)]
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
    [DataContract]
    public class AspnetPersonalizationAllUsers
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid PathId { get; set; } // PathId (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public byte[] PageSettings { get; set; } // PageSettings

        [DataMember(Order = 3, IsRequired = false)]
        public DateTime LastUpdatedDate { get; set; } // LastUpdatedDate


        // Foreign keys
        public virtual AspnetPaths AspnetPaths { get; set; } //  PathId - FK__aspnet_Pe__PathI__628FA481
    }

    // aspnet_PersonalizationPerUser
    [DataContract]
    public class AspnetPersonalizationPerUser
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid Id { get; set; } // Id (Primary key)

        [DataMember(Order = 2, IsRequired = true)]
        public Guid? PathId { get; set; } // PathId

        [DataMember(Order = 3, IsRequired = true)]
        public Guid? UserId { get; set; } // UserId

        [DataMember(Order = 4, IsRequired = false)]
        public byte[] PageSettings { get; set; } // PageSettings

        [DataMember(Order = 5, IsRequired = false)]
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
    [DataContract]
    public class AspnetProfile
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid UserId { get; set; } // UserId (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public string PropertyNames { get; set; } // PropertyNames

        [DataMember(Order = 3, IsRequired = false)]
        public string PropertyValuesString { get; set; } // PropertyValuesString

        [DataMember(Order = 4, IsRequired = false)]
        public byte[] PropertyValuesBinary { get; set; } // PropertyValuesBinary

        [DataMember(Order = 5, IsRequired = false)]
        public DateTime LastUpdatedDate { get; set; } // LastUpdatedDate


        // Foreign keys
        public virtual AspnetUsers AspnetUsers { get; set; } //  UserId - FK__aspnet_Pr__UserI__38996AB5
    }

    // aspnet_Roles
    [DataContract]
    public class AspnetRoles
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid ApplicationId { get; set; } // ApplicationId

        [DataMember(Order = 2, IsRequired = false)]
        public Guid RoleId { get; set; } // RoleId (Primary key)

        [DataMember(Order = 3, IsRequired = false)]
        public string RoleName { get; set; } // RoleName

        [DataMember(Order = 4, IsRequired = false)]
        public string LoweredRoleName { get; set; } // LoweredRoleName

        [DataMember(Order = 5, IsRequired = true)]
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
    [DataContract]
    public class AspnetSchemaVersions
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string Feature { get; set; } // Feature (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public string CompatibleSchemaVersion { get; set; } // CompatibleSchemaVersion (Primary key)

        [DataMember(Order = 3, IsRequired = false)]
        public bool IsCurrentVersion { get; set; } // IsCurrentVersion

    }

    // aspnet_Users
    [DataContract]
    public class AspnetUsers
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid ApplicationId { get; set; } // ApplicationId

        [DataMember(Order = 2, IsRequired = false)]
        public Guid UserId { get; set; } // UserId (Primary key)

        [DataMember(Order = 3, IsRequired = false)]
        public string UserName { get; set; } // UserName

        [DataMember(Order = 4, IsRequired = false)]
        public string LoweredUserName { get; set; } // LoweredUserName

        [DataMember(Order = 5, IsRequired = true)]
        public string MobileAlias { get; set; } // MobileAlias

        [DataMember(Order = 6, IsRequired = false)]
        public bool IsAnonymous { get; set; } // IsAnonymous

        [DataMember(Order = 7, IsRequired = false)]
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
    [DataContract]
    public class AspnetUsersInRoles
    {
        [DataMember(Order = 1, IsRequired = false)]
        public Guid UserId { get; set; } // UserId (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public Guid RoleId { get; set; } // RoleId (Primary key)


        // Foreign keys
        public virtual AspnetUsers AspnetUsers { get; set; } //  UserId - FK__aspnet_Us__UserI__49C3F6B7
        public virtual AspnetRoles AspnetRoles { get; set; } //  RoleId - FK__aspnet_Us__RoleI__4AB81AF0
    }

    // aspnet_WebEvent_Events
    [DataContract]
    public class AspnetWebEventEvents
    {
        [DataMember(Order = 1, IsRequired = false)]
        public string EventId { get; set; } // EventId (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public DateTime EventTimeUtc { get; set; } // EventTimeUtc

        [DataMember(Order = 3, IsRequired = false)]
        public DateTime EventTime { get; set; } // EventTime

        [DataMember(Order = 4, IsRequired = false)]
        public string EventType { get; set; } // EventType

        [DataMember(Order = 5, IsRequired = false)]
        public decimal EventSequence { get; set; } // EventSequence

        [DataMember(Order = 6, IsRequired = false)]
        public decimal EventOccurrence { get; set; } // EventOccurrence

        [DataMember(Order = 7, IsRequired = false)]
        public int EventCode { get; set; } // EventCode

        [DataMember(Order = 8, IsRequired = false)]
        public int EventDetailCode { get; set; } // EventDetailCode

        [DataMember(Order = 9, IsRequired = true)]
        public string Message { get; set; } // Message

        [DataMember(Order = 10, IsRequired = true)]
        public string ApplicationPath { get; set; } // ApplicationPath

        [DataMember(Order = 11, IsRequired = true)]
        public string ApplicationVirtualPath { get; set; } // ApplicationVirtualPath

        [DataMember(Order = 12, IsRequired = false)]
        public string MachineName { get; set; } // MachineName

        [DataMember(Order = 13, IsRequired = true)]
        public string RequestUrl { get; set; } // RequestUrl

        [DataMember(Order = 14, IsRequired = true)]
        public string ExceptionType { get; set; } // ExceptionType

        [DataMember(Order = 15, IsRequired = true)]
        public string Details { get; set; } // Details

    }


    // ************************************************************************
    // POCO Configuration

    // aspnet_Applications
    internal class AspnetApplicationsConfiguration : EntityTypeConfiguration<AspnetApplications>
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
    internal class AspnetMembershipConfiguration : EntityTypeConfiguration<AspnetMembership>
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
    internal class AspnetPathsConfiguration : EntityTypeConfiguration<AspnetPaths>
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
    internal class AspnetPersonalizationAllUsersConfiguration : EntityTypeConfiguration<AspnetPersonalizationAllUsers>
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
    internal class AspnetPersonalizationPerUserConfiguration : EntityTypeConfiguration<AspnetPersonalizationPerUser>
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
    internal class AspnetProfileConfiguration : EntityTypeConfiguration<AspnetProfile>
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
    internal class AspnetRolesConfiguration : EntityTypeConfiguration<AspnetRoles>
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
    internal class AspnetSchemaVersionsConfiguration : EntityTypeConfiguration<AspnetSchemaVersions>
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
    internal class AspnetUsersConfiguration : EntityTypeConfiguration<AspnetUsers>
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
    internal class AspnetUsersInRolesConfiguration : EntityTypeConfiguration<AspnetUsersInRoles>
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
    internal class AspnetWebEventEventsConfiguration : EntityTypeConfiguration<AspnetWebEventEvents>
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


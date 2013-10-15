using System;
using System.Data.Entity;
using EntityFramework_Reverse_POCO_Generator;

namespace Tester.UnitTest
{
    public class FakeDbContext : IMyDbContext
    {
        public IDbSet<AspnetApplications> AspnetApplications { get; set; }
        public IDbSet<AspnetMembership> AspnetMembership { get; set; }
        public IDbSet<AspnetPaths> AspnetPaths { get; set; }
        public IDbSet<AspnetPersonalizationAllUsers> AspnetPersonalizationAllUsers { get; set; }
        public IDbSet<AspnetPersonalizationPerUser> AspnetPersonalizationPerUser { get; set; }
        public IDbSet<AspnetProfile> AspnetProfile { get; set; }
        public IDbSet<AspnetRoles> AspnetRoles { get; set; }
        public IDbSet<AspnetSchemaVersions> AspnetSchemaVersions { get; set; }
        public IDbSet<AspnetUsers> AspnetUsers { get; set; }
        public IDbSet<AspnetUsersInRoles> AspnetUsersInRoles { get; set; }
        public IDbSet<AspnetWebEventEvents> AspnetWebEventEvents { get; set; }
        public IDbSet<VwAspnetApplications> VwAspnetApplications { get; set; }
        public IDbSet<VwAspnetMembershipUsers> VwAspnetMembershipUsers { get; set; }
        public IDbSet<VwAspnetProfiles> VwAspnetProfiles { get; set; }
        public IDbSet<VwAspnetRoles> VwAspnetRoles { get; set; }
        public IDbSet<VwAspnetUsers> VwAspnetUsers { get; set; }
        public IDbSet<VwAspnetUsersInRoles> VwAspnetUsersInRoles { get; set; }
        public IDbSet<VwAspnetWebPartStatePaths> VwAspnetWebPartStatePaths { get; set; }
        public IDbSet<VwAspnetWebPartStateShared> VwAspnetWebPartStateShared { get; set; }
        public IDbSet<VwAspnetWebPartStateUser> VwAspnetWebPartStateUser { get; set; }

        public FakeDbContext()
        {
            AspnetApplications = new FakeDbSet<AspnetApplications>();
            AspnetMembership = new FakeDbSet<AspnetMembership>();
            AspnetPaths = new FakeDbSet<AspnetPaths>();
            AspnetPersonalizationAllUsers = new FakeDbSet<AspnetPersonalizationAllUsers>();
            AspnetPersonalizationPerUser = new FakeDbSet<AspnetPersonalizationPerUser>();
            AspnetProfile = new FakeDbSet<AspnetProfile>();
            AspnetRoles = new FakeDbSet<AspnetRoles>();
            AspnetSchemaVersions = new FakeDbSet<AspnetSchemaVersions>();
            AspnetUsers = new FakeDbSet<AspnetUsers>();
            AspnetUsersInRoles = new FakeDbSet<AspnetUsersInRoles>();
            AspnetWebEventEvents = new FakeDbSet<AspnetWebEventEvents>();
            VwAspnetApplications = new FakeDbSet<VwAspnetApplications>();
            VwAspnetMembershipUsers = new FakeDbSet<VwAspnetMembershipUsers>();
            VwAspnetProfiles = new FakeDbSet<VwAspnetProfiles>();
            VwAspnetRoles = new FakeDbSet<VwAspnetRoles>();
            VwAspnetUsers = new FakeDbSet<VwAspnetUsers>();
            VwAspnetUsersInRoles = new FakeDbSet<VwAspnetUsersInRoles>();
            VwAspnetWebPartStatePaths = new FakeDbSet<VwAspnetWebPartStatePaths>();
            VwAspnetWebPartStateShared = new FakeDbSet<VwAspnetWebPartStateShared>();
            VwAspnetWebPartStateUser = new FakeDbSet<VwAspnetWebPartStateUser>();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public void Dispose()
        {
            throw new NotImplementedException(); 
        }
    }
}
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
        }

        public int SaveChanges()
        {
            return 1;
        }
    }
}
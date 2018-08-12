using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace OSNB.Models
{
    public class AppDbContext : DbContext
    {
        // Security tables
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Member> Members { get; set; }
        public DbSet<MemberBloodGroup> MemberBloodGroups { get; set; }
        public DbSet<MemberDistrict> MemberDistricts { get; set; }
        public DbSet<MemberDonate> MemberDonates { get; set; }
        public DbSet<MemberDonateType> MemberDonateTypes { get; set; }
        public DbSet<MemberDonateWay> MemberDonateWays { get; set; }
        public DbSet<MemberHospital> MemberHospitals { get; set; }
        public DbSet<MemberStatus> MemberStatues { get; set; }
        public DbSet<MemberZone> MemberZones { get; set; }

        public DbSet<SendEmailInfo> SendEmailInfos { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Maps to the expected many-to-many join table name for roles to users.
            modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .Map(m =>
            {
                m.ToTable("RoleMemberships");
                m.MapLeftKey("UserName");
                m.MapRightKey("RoleName");
            });

            //one to one relationship with user mapping
            modelBuilder.Entity<User>()
            .HasOptional(u => u.Member)
            .WithMany()
            .HasForeignKey(u => u.MemberId);

            //one to one relationship with profile mapping
            modelBuilder.Entity<Member>()
            .HasRequired(u => u.User)
            .WithMany()
            .HasForeignKey(u => u.UserName);

            // Maps to the expected many-to-many join table name for MemberStatus to Member.
            modelBuilder.Entity<Member>()
            .HasMany(u => u.MemberStatues)
            .WithMany(r => r.Members)
            .Map(m =>
            {
                m.ToTable("MemberMemberStatues");
                m.MapLeftKey("MemberId");
                m.MapRightKey("MemberStatusId");
            });

            modelBuilder.Entity<MemberZone>()
                    .HasRequired(m => m.MemberDistrict)
                    .WithMany()
                    .WillCascadeOnDelete(false);

            modelBuilder.Entity<MemberHospital>()
                        .HasRequired(m => m.MemberZone)
                        .WithMany()
                        .WillCascadeOnDelete(false);

        }

    }

    #region Initial data

    // Change the base class as follows if you want to drop and create the database during development:
    //public class DbInitializer : DropCreateDatabaseAlways<AppDbContext>
    //public class DbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        private static void CreateUserWithRole(string username, string password, string email, string rolename, AppDbContext context)
        {
            var status = new MembershipCreateStatus();

            Membership.CreateUser(username, password, email);
            if (status == MembershipCreateStatus.Success)
            {
                // Add the role.
                var user = context.Users.Find(username);
                var adminRole = context.Roles.Find(rolename);
                user.Roles = new List<Role> { adminRole };
            }
        }


        protected override void Seed(AppDbContext context)
        {
            // Create default roles.
            var roles = new List<Role>
                            {
                                new Role {RoleName = "Admin"},
                                new Role {RoleName = "User"}
                            };

            roles.ForEach(r => context.Roles.Add(r));

            context.SaveChanges();

            // Create some users.
            CreateUserWithRole("Rasel", "@123456", "raselahmmed@gmail.com", "Admin", context);
            CreateUserWithRole("Ahmmed", "@123456", "raselahmmed@gmail.com", "Admin", context);
            CreateUserWithRole("Sohel", "@123456", "sohel@gmail.com", "User", context);
            CreateUserWithRole("Shafin", "@123456", "shafin@gmail.com", "User", context);

            context.SaveChanges();

        }
    }

    #endregion
}
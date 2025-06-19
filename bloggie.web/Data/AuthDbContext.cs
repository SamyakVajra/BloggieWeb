using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Seed Roles (User, Admin, SuperAdmin)
            var adminRoleId = "71c9ba41-3e7a-46fa-b412-b5d975a70b57";
            var superAdminRoleId = "913216e1-c393-4ea7-a815-e5dc38cc1b09";
            var userRoleId = "f6298a06-7f3d-4c1f-98be-dd08868deaee";

            var roles = new List<IdentityRole> {
               new IdentityRole
               {
                   Name = "Admin",
                   NormalizedName = "Admin",
                   Id = adminRoleId,
                   ConcurrencyStamp = adminRoleId
               },
               new IdentityRole
               {
                   Name = "SuperAdmin",
                   NormalizedName = "SuperAdmin",
                   Id = superAdminRoleId,
                   ConcurrencyStamp = superAdminRoleId
               },
                new IdentityRole
               {
                   Name = "User",
                   NormalizedName = "User",
                   Id = userRoleId,
                   ConcurrencyStamp = userRoleId
               }

            };

            builder.Entity<IdentityRole>().HasData(roles);

            //Seed SuperAdminUser
            var superAdminId = "cbf40934-44b4-42f6-a307-9d3551a7cf03";

            //var superAdminUser = new IdentityUser
            //{
            //    Id = superAdminId,
            //    UserName = "superadmin@bloggie.com",
            //    Email = "superadmin@bloggie.com",
            //    NormalizedEmail = "SUPERADMIN@BLOGGIE.COM",
            //    NormalizedUserName = "SUPERADMIN@BLOGGIE.COM",
            //    PasswordHash = "AQAAAAEAACcQAAAAEGixm9zNrsLrXEFV9E7pQaGlc0vjwTuD1yA0dlbd8P0Yg1Z9dXACqdcZDWZqpRoVmg==" // precomputed value
            //};
            // password: User@123

            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com",
                Id = superAdminId

            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //Add All roles to SuperAdminUser

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);


        }
    }
} 

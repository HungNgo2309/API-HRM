using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext:IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<StaffRelate> StaffRelates { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ReasonCategory> ResonCategorys { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            // {
            //     entity.HasKey(login => new { login.LoginProvider, login.ProviderKey });
            // });

            // modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            // {
            //     entity.HasKey(role => new { role.UserId, role.RoleId });
            // });

            // modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            // {
            //     entity.HasKey(token => new { token.UserId, token.LoginProvider, token.Name });
            // });
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
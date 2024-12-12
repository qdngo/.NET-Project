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
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));

            builder.Entity<Portfolio>()
             .HasOne(u => u.AppUser)
             .WithMany(u => u.Portfolios)
             .HasForeignKey(p => p.AppUserId);

            builder.Entity<Portfolio>()
             .HasOne(u => u.Stock)
             .WithMany(u => u.Portfolios)
             .HasForeignKey(p => p.StockId);

            // Predefined GUIDs for roles
            var adminRoleId = Guid.Parse("d3f3c5c0-3dbf-4b56-b243-91c3c30d845f");
            var userRoleId = Guid.Parse("78f2955c-6d13-431e-a879-525ae1f55a15");

            // Seed roles with consistent GUIDs
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId.ToString(), // Use predefined GUID for Admin role
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = adminRoleId.ToString()
                },
                new IdentityRole
                {
                    Id = userRoleId.ToString(), // Use predefined GUID for User role
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = userRoleId.ToString()
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
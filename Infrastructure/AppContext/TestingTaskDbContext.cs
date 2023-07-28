using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AppContext
{
    public class TestingTaskDbContext : IdentityDbContext<ApplicationUser>
    {
        public TestingTaskDbContext(DbContextOptions<TestingTaskDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Seed Data

            string ADMIN_ID = "02174cf0–9412–4cfe - afbf - 59f706d72cf6";
            string ROLE_ID = "341743f0 - asd2–42de - afbf - 59kmkkmk72cf6";

            //seed admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN”",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            //create user
            var appUser = new ApplicationUser
            {
                Id = ADMIN_ID,
                Email = "frankofoedu@gmail.com",
                EmailConfirmed = true,
                UserName = "frankofoedu@gmail.com",
                NormalizedUserName = "FRANKOFOEDU@GMAIL.COM",
                PhoneNumber = "1234567890",
                FirstName = "Frank",
                LastName = "Oedu",
                City = "Berlin"
            };

            //set user password
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, "123456");

            //seed user
            builder.Entity<ApplicationUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

            //seed product
            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = "02174cf0–9412–5sfe - afbf - 59f706d72cf6",
                    IsAvailable = true,
                    ManufactorEmail = appUser.Email,
                    Name = "محصول اول",
                    ProduceDate = DateTime.Now,
                    UserId = "02174cf0–9412–4cfe - afbf - 59f706d72cf6",
                    ManufactorPhone = appUser.PhoneNumber
                });

            #endregion

            //Check ManufactorEmail be unique
            builder.Entity<Product>()
                .HasIndex(p => p.ManufactorEmail)
                .IsUnique();

            //check ProductDate be unique
            builder.Entity<Product>()
                .HasIndex(p => p.ProduceDate)
                .IsUnique();
        }
    }
}

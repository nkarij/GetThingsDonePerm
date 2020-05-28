using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YDIAB.Models;

namespace YDIAB.Data
{
    public class AppDbContext : IdentityDbContext<StoreUser>
    {

        private readonly IConfiguration _config;

        // this is to get the appsetting.json, fx the connectionstring.
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        public DbSet<List> Lists { get; set; }

        public DbSet<Item> ListItems { get; set; }

        public DbSet<Tag> ListTags { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var newUser = new StoreUser
            {
                FirstName = "Nanna",
                LastName = "Jensen",
                UserName = "tester@test.com",
                Password = "nanna1234",
                Email = "tester@test.com",
                NormalizedEmail = "tester@test.com",
                NormalizedUserName = "tester",
                EmailConfirmed = true,
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = false,
            };

            PasswordHasher<StoreUser> ph = new PasswordHasher<StoreUser>();
            newUser.PasswordHash = ph.HashPassword(newUser, "nanna1234");

            //modelBuilder.Entity<IdentityRole>().HasData(
            //    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            //    new IdentityRole { Name = "Guest", NormalizedName = "GUEST" }
            //);

            modelBuilder.Entity<StoreUser>()
                .HasData(newUser);

            // seed data List
            modelBuilder.Entity<List>().HasData(new List
            {
                Id = 1,
                Name = "List nr 1",
                Description = "list nr 1 description",
            });
        }


    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resturant.Models;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Resturant.Data
{


    public class YummyDbContext : IdentityDbContext<User>
    {
        public YummyDbContext(DbContextOptions<YummyDbContext> options)  : base ( options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1s2a3d4m5i6n7", Name = "Super Admin", NormalizedName = "Super Admin" },
                new IdentityRole { Id = "1a2d3m4i5n6", Name = "Admin", NormalizedName = "Admin" },
                new IdentityRole { Id = "1u2s3e4r5", Name = "User", NormalizedName = "User" }
            );



            modelBuilder.Entity<User>().HasData(
                new User { Id = "1", FullName = "Rafeeq Super Admin",  UserName = "rafeeqsa@gmail.com", NormalizedUserName="RAFEEQSA@GMAIL.COM", Email = "rafeeqsa@gmail.com", NormalizedEmail="RAFEEQSA@GMAIL.COM", PhoneNumber = "+972595385030", PasswordHash = "43211234Aa@" },
                new User { Id = "2", FullName = "Rafeeq Admin",  UserName = "rafeeqa@gmail.com", NormalizedUserName="RAFEEQA@GMAIL.COM", Email = "rafeeqa@gmail.com", NormalizedEmail="RAFEEQA@GMAIL.COM", PhoneNumber = "+972595385030", PasswordHash = "43211234Aa@" }
//                new User { Id = "3", FullName = "Rafeeq User", UserName = "rafeequ@gmail.com", NormalizedUserName="RAFEEQU@GMAIL.COM", Email = "rafeequ@gmail.com", NormalizedEmail="RAFEEQALALOUL@GMAIL.COM", PhoneNumber = "+972595385030", PasswordHash = "43211234Aa@" }
            );



            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "1", RoleId = "1s2a3d4m5i6n7" },
                new IdentityUserRole<string> { UserId = "2", RoleId = "1a2d3m4i5n6" }
//                new IdentityUserRole<string> { UserId = "3", RoleId = "1u2s3e4r5" }
            );




            modelBuilder.Entity<Meat>().HasData(
                new Meat { Id = 1, Name = "المقبلات" },
                new Meat { Id = 2, Name = "الفطور" },
                new Meat { Id = 3, Name = "الغداء" },
                new Meat { Id = 4, Name = "العشاء" }
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Menu> menus { get; set; }
        public DbSet<Meat> meats { get; set; }
        public DbSet<Chefs> chefs { get; set; }
        public DbSet<Contact> contacts { get; set; }
        public DbSet<Events> events { get; set; }
        public DbSet<ShoppingCartItem> shoppingCartItems { get; set; }

    }
}

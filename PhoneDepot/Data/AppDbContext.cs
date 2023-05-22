using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhoneDepot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace PhoneDepot.Data
{

    //public class AppDbContext : DbContext
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public async Task<PhoneManufacturer> GetByIdWithPhonesAsync(int id)
        {
            return await this.PhoneManufacturer
                .Include(pm => pm.Phone)
                .SingleOrDefaultAsync(pm => pm.Id == id);
        }

  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Phone>()
                    .HasOne(p => p.PhoneManufacturer)     // A Phone has one PhoneManufacturer
                    .WithMany(pm => pm.Phone)            // A PhoneManufacturer can have many Phones
                    .HasForeignKey(p => p.ManufacturerId); // The foreign key property on Phone is ManufacturerId



            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => l.UserId);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });


            });


            //Stopp
            //modelBuilder.Entity<IdentityRole>(entity =>
            //{
            //    entity.Property(e => e.Name).IsRequired();
            //});


        }

        //Products
        public DbSet<Phone> Phone { get; set; }
        public DbSet<PhoneManufacturer> PhoneManufacturer { get; set; }



        // Orders
        public DbSet<Order> Order { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

    }

}


//// Set primary key of IdentityUserLogin to null
//modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
//{
//    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
//    entity.Property(e => e.Id).ValueGeneratedNever();
//});

//base.OnModelCreating(modelBuilder);
//                    }

//protected override void OnModelCreating(ModelBuilder modelBuilder)
//{


//    // Your existing code here
//    modelBuilder.Entity<Phone>()
//        .HasOne(p => p.PhoneManufacturer)
//        .WithMany(pm => pm.Phone)
//        .HasForeignKey(p => p.ManufacturerId);



//    base.OnModelCreating(modelBuilder);

//    modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
//    {
//        entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
//    });
//}

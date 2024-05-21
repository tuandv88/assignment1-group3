using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject {
    public class TDbContext : DbContext {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Staff> Staffs { get; set; }

        public TDbContext() {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var currentDirectory = Directory.GetCurrentDirectory();
            var parentDirectory = Directory.GetParent(currentDirectory).FullName;

            var builder = new ConfigurationBuilder()
                .SetBasePath(parentDirectory + "/BusinessObject")
                .AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Ass1_Prn231"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) {
                relationship.DeleteBehavior = DeleteBehavior.SetNull;
            }

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { CategoryId = 1, CategoryName = "Beverages" },
                    new Category { CategoryId = 2, CategoryName = "Condiments" },
                    new Category { CategoryId = 3, CategoryName = "Confections" },
                    new Category { CategoryId = 4, CategoryName = "Dairy Products" },
                    new Category { CategoryId = 5, CategoryName = "Grains/Cereals" },
                    new Category { CategoryId = 6, CategoryName = "Meat/Poultry" },
                    new Category { CategoryId = 7, CategoryName = "Produce" },
                    new Category { CategoryId = 8, CategoryName = "Seafood" }
                );
        }
    }
}

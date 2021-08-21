#nullable disable

using Microsoft.EntityFrameworkCore;
using MRybka.FirstLineSoftware.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MRybka.FirstLineSoftware.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            this.Database.EnsureCreated();
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 1,
                Name = "Vase",
                Price = 1.2m
            });
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 2,
                Name = "Big mug",
                Price = 1m,
                DiscountedAmount = 2,
                DiscountedPrice = 1.5m
            });
            modelBuilder.Entity<Product>().HasData(new Product()
            {
                Id = 3,
                Name = "Napkins pack",
                Price = 0.45m,
                DiscountedAmount = 3,
                DiscountedPrice = 0.9m
            });
        }

        public virtual DbSet<BasketItem> BasketItems { get; set;}
        public virtual DbSet<Product> Products { get; set; }
    }
}

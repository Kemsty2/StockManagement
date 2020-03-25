using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockManagement.Repository.Entities
{
    public class StockManagementContext : DbContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Order> Order { get; set; }

        public StockManagementContext(DbContextOptions<StockManagementContext> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(new Company()
            {
                CompanyId=1,
                CompanyName = "Apple Inc",
                StockQuantity= 100,
                Price=50,
                Status=true
            },
            new Company()
            {
                CompanyId = 2,
                CompanyName = "Google Inc",
                StockQuantity = 200,
                Price = 150,
                Status = true
            },
            new Company()
            {
                CompanyId = 3,
                CompanyName = "Microsoft Inc",
                StockQuantity = 50,
                Price = 100,
                Status = true
            }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}

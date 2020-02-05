using Microsoft.EntityFrameworkCore;
using ProductMan.API.Domain.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMan.API.Domain.Context
{
    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductMan.API.Domain.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMan.API.Domain.Context
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.ProductID);

            builder.Property(p => p.Code).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.Name).HasColumnType("nvarchar(200)").IsRequired();
        }
    }
}
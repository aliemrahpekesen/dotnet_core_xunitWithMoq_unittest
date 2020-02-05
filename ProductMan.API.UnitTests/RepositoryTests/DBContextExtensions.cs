using ProductMan.API.Domain.Context;
using ProductMan.API.Domain.Context.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ProductMan.API.UnitTests.RepositoryTests
{
    public static class DbContextExtensions
    {
        public static void Seed(this ProductDBContext dbContext)
        {
            IList<Product> products = new List<Product>();
            products.Add(new Product
            {
                ProductID = 1000,
                Code = "A1",
                Name = "A1X1",
                TaxRate = 18,
                UnitPrice = 10,
                StockCount = 10
            });

            products.Add(new Product
            {
                ProductID = 2000,
                Code = "A2",
                Name = "A2X1",
                TaxRate = 18,
                UnitPrice = 20,
                StockCount = 20
            });

            products.Add(new Product
            {
                ProductID = 3000,
                Code = "A3",
                Name = "A3X1",
                TaxRate = 18,
                UnitPrice = 30,
                StockCount = 30
            });
            dbContext.Set<Product>().AddRange(products);
            dbContext.SaveChanges();
        }

        public static void Clear(this ProductDBContext dbContext)
        {
            dbContext.Set<Product>().RemoveRange(dbContext.Set<Product>());
            dbContext.SaveChanges();
        }
    }
}
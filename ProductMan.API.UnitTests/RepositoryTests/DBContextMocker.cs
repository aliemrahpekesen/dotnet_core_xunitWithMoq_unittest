using Microsoft.EntityFrameworkCore;
using ProductMan.API.Domain.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductMan.API.UnitTests.RepositoryTests
{
    public static class DBContextMocker
    {
        public static ProductDBContext GetMockedProductDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ProductDBContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            var dbContext = new ProductDBContext(options);
            dbContext.Clear();
            dbContext.Seed();
            return dbContext;
        }
    }
}
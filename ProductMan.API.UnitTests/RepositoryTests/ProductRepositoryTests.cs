using ProductMan.API.Domain.Context.Entities;
using ProductMan.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ProductMan.API.UnitTests.RepositoryTests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void Should_ReturnAllProducts_When_GetAllIsCalled()
        {
            var dbContext = DBContextMocker.GetMockedProductDbContext("productDB");
            var productRepository = new ProductRepository(dbContext);

            var allProducts = productRepository.GetAll().AsEnumerable<Product>().ToList();

            Assert.NotEmpty(allProducts);
        }

        [Theory]
        [InlineData(1000, "A1")]
        [InlineData(2000, "A2")]
        [InlineData(3000, "A3")]
        public void Should_ReturnCorrectProduct_When_GetByIdIsCalled(int id, string code)
        {
            var dbContext = DBContextMocker.GetMockedProductDbContext("productDB");
            var productRepository = new ProductRepository(dbContext);

            var matchedProduct = productRepository.GetById(id).Result;

            Assert.NotNull(matchedProduct);
            Assert.Equal(id, matchedProduct.ProductID);
            Assert.Equal(code, matchedProduct.Code);
        }

        [Theory]
        [InlineData(1000, "AX")]
        [InlineData(2000, "AX")]
        [InlineData(3000, "AX")]
        public void Should_Not_ReturnCorrectProduct_When_GetByIdIsCalled(int id, string code)
        {
            var dbContext = DBContextMocker.GetMockedProductDbContext("productDB");
            var productRepository = new ProductRepository(dbContext);

            var matchedProduct = productRepository.GetById(id).Result;

            Assert.NotNull(matchedProduct);
            Assert.Equal(id, matchedProduct.ProductID);
            Assert.NotEqual(code, matchedProduct.Code);
        }
    }
}
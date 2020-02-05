using Moq;
using ProductMan.API.Domain.Context.Entities;
using ProductMan.API.Repositories;
using ProductMan.API.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductMan.API.UnitTests.ServiceTests
{
    public class ProductServiceTests
    {
        #region Preparation

        private ICollection<Product> productList;

        public ICollection<Product> PrepareProductList()
        {
            this.productList = new LinkedList<Product>();
            this.productList.Add(new Product() { ProductID = 1, Code = "A1", Name = "A1X1", TaxRate = 18, UnitPrice = 10, StockCount = 10 });
            this.productList.Add(new Product() { ProductID = 2, Code = "A2", Name = "A2X1", TaxRate = 18, UnitPrice = 20, StockCount = 20 });
            this.productList.Add(new Product() { ProductID = 3, Code = "A3", Name = "A3X1", TaxRate = 18, UnitPrice = 30, StockCount = 30 });
            return this.productList;
        }

        public Product GetSampleProduct()
        {
            PrepareProductList();
            var product = ((LinkedList<Product>)this.productList).First.Value;
            return product;
        }

        private Product GetCreateResult()
        {
            return new Product()
            {
                ProductID = 1000,
                Code = "NCP",
                Name = "NCP_Sample",
                TaxRate = 18,
                UnitPrice = 10,
                StockCount = 10
            };
        }

        #endregion Preparation

        [Fact]
        public void Should_ReturnAProduct_When_CreateIsCalled()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(GetCreateResult()));

            var service = new ProductService(productRepositoryMock.Object);

            Product persistedObject = new Product()
            {
                Code = "NCP",
                Name = "NCP_Sample",
                TaxRate = 18,
                UnitPrice = 10,
                StockCount = 10
            };

            var response = service.Create(persistedObject);
            var createdResource = response.Result;

            Assert.IsType<Product>(createdResource);
        }

        [Fact]
        public void Should_ReturnAProductWithId_When_CreateIsCalled()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(GetCreateResult()));

            var service = new ProductService(productRepositoryMock.Object);

            Product persistedObject = new Product()
            {
                Code = "NCP",
                Name = "NCP_Sample",
                TaxRate = 18,
                UnitPrice = 10,
                StockCount = 10
            };

            var response = service.Create(persistedObject);
            var createdResource = response.Result;

            var createdResourceId = createdResource.ProductID;
            Assert.NotNull(createdResourceId.ToString());
            Assert.Equal(1000, createdResourceId);
        }

        [Fact]
        public void Should_ReturnDuplicateError_When_ExistingResourceCreationIsCalled()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetByCode(It.IsAny<string>()))
                .Returns(GetCreateResult());

            var service = new ProductService(productRepositoryMock.Object);

            var result = service.DuplicateCheckByCode("A1");
            Assert.True(result);
        }

        [Fact]
        public void Should_Not_ReturnDuplicateError_When_NonExistingResourceCreationIsCalled()
        {
            Product returnValue = null;
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetByCode(It.IsAny<string>()))
                .Returns(returnValue);

            var service = new ProductService(productRepositoryMock.Object);

            var result = service.DuplicateCheckByCode("A1");
            Assert.False(result);
        }

        [Theory]
        [InlineData(1000, "A1")]
        [InlineData(2000, "A2")]
        public void Should_ReturnMatchedProduct_When_GetByIdIsCalled(int id, string code)
        {
            Product p1 = new Product() { ProductID = 1000, Code = "A1" };
            Product p2 = new Product() { ProductID = 2000, Code = "A2" };

            var productRepositoryMock = new Mock<IProductRepository>();

            if (id == 1000)
                productRepositoryMock.Setup(repo => repo.GetById(id))
                    .Returns(Task.FromResult(p1));
            if (id == 2000)
                productRepositoryMock.Setup(repo => repo.GetById(id))
                .Returns(Task.FromResult(p2));

            var service = new ProductService(productRepositoryMock.Object);

            var response = service.RetrieveProductById(id);
            Assert.True(response.Result.Code == code);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductMan.API.Controllers;
using ProductMan.API.Domain.Context.Entities;
using ProductMan.API.Domain.Model;
using ProductMan.API.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductMan.API.UnitTests.ControllerTests
{
    public class ProductControllerTests
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
        public void Should_ReturnCreatedProductResourceResponse_When_PostIsCalled()
        {
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(svc => svc.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(GetCreateResult()));

            var controller = new ProductController(Mock.Of<ILogger<ProductController>>(), productServiceMock.Object);

            PostProductRequest request = new PostProductRequest()
            {
                Code = "NCP",
                Name = "NCP_Sample",
                TaxRate = 18,
                UnitPrice = 10,
                StockCount = 10
            };

            var response = controller.PostProductAsync(request);
            var createdResource = (OkObjectResult)response.Result;

            Assert.IsType<Product>(createdResource.Value);
        }

        [Fact]
        public void Should_IdIsEqualTo1000InResponse_When_ProductPostIsCalled()
        {
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(svc => svc.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(GetCreateResult()));

            var controller = new ProductController(Mock.Of<ILogger<ProductController>>(), productServiceMock.Object);

            PostProductRequest request = new PostProductRequest()
            {
                Code = "NCP",
                Name = "NCP_Sample",
                TaxRate = 18,
                UnitPrice = 10,
                StockCount = 10
            };

            var response = controller.PostProductAsync(request);
            var createdResource = (OkObjectResult)response.Result;

            Assert.NotNull(createdResource);
            var createdResourceId = ((Product)createdResource.Value).ProductID;
            Assert.Equal(1000, createdResourceId);
        }

        [Fact]
        public void ShouldNot_BeNullIdInResponse_When_ProductPostIsCalled()
        {
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(svc => svc.Create(It.IsAny<Product>()))
                .Returns(Task.FromResult(GetCreateResult()));

            var controller = new ProductController(Mock.Of<ILogger<ProductController>>(), productServiceMock.Object);

            PostProductRequest request = new PostProductRequest()
            {
                Code = "NCP",
                Name = "NCP_Sample",
                TaxRate = 18,
                UnitPrice = 10,
                StockCount = 10
            };

            var response = controller.PostProductAsync(request);
            var createdResource = (OkObjectResult)response.Result;

            Assert.NotNull(createdResource);
            var createdResourceId = ((Product)createdResource.Value).ProductID;
            Assert.NotNull(createdResourceId.ToString());
        }

        [Fact]
        public void Should_ReturnListOfProducts_When_GetAllIsCalled()
        {
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(svc => svc.RetrieveAllProducts())
                .Returns(PrepareProductList());

            var controller = new ProductController(Mock.Of<ILogger<ProductController>>(), productServiceMock.Object);

            var response = controller.GetProductsAsync();
            var createdResource = (OkObjectResult)response.Result;

            Assert.NotEmpty((LinkedList<Product>)createdResource.Value);
            Assert.Equal(3, ((LinkedList<Product>)createdResource.Value).Count);
        }

        [Fact]
        public void Should_ReturnOneProductBy_Given_Id_When_GetByIdIsCalled()
        {
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(svc => svc.RetrieveProductById(It.IsAny<int>()))
                .Returns(Task.FromResult<Product>(GetSampleProduct()));

            var controller = new ProductController(Mock.Of<ILogger<ProductController>>(), productServiceMock.Object);

            var response = controller.GetProductByIdAsync(1);
            var createdResource = (OkObjectResult)response.Result;

            Assert.IsType<Product>((Product)createdResource.Value);
            Assert.Equal("A1", ((Product)createdResource.Value).Code);
        }
    }
}
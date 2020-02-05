using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductMan.API.Domain.Context.Entities;
using ProductMan.API.Repositories;

namespace ProductMan.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<Product> Create(Product entity)
        {
            return await this._productRepository.Create(entity);
        }

        public async Task Delete(int id)
        {
            await this._productRepository.Delete(id);
        }

        public bool DuplicateCheckByCode(string code)
        {
            var foundEntity = this._productRepository.GetByCode(code);
            return foundEntity != null;
        }

        public ICollection<Product> RetrieveAllProducts()
        {
            return this._productRepository.GetAll().AsEnumerable<Product>().ToList();
        }

        public async Task<Product> RetrieveProductById(int id)
        {
            return await this._productRepository.GetById(id);
        }

        public async Task<Product> Update(int id, Product entity)
        {
            if (entity.ProductID == id)
                return await this._productRepository.Update(id, entity);
            else return null;
        }
    }
}
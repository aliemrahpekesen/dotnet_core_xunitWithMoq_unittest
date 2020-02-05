using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductMan.API.Domain.Context;
using ProductMan.API.Domain.Context.Entities;

namespace ProductMan.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDBContext _dbContext;

        public ProductRepository(ProductDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Product> Create(Product entity)
        {
            await this._dbContext.Set<Product>().AddAsync(entity);
            await this._dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            this._dbContext.Set<Product>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Product> GetAll()
        {
            return this._dbContext.Set<Product>().AsNoTracking();
        }

        public Product GetByCode(string code)
        {
            return this._dbContext.Set<Product>()
                .FirstOrDefault(p => code.Equals(p.Code));
        }

        public async Task<Product> GetById(int id)
        {
            return await this._dbContext.Set<Product>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductID == id);
        }

        public async Task<Product> Update(int id, Product entity)
        {
            this._dbContext.Set<Product>().Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
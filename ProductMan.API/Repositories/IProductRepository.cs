using ProductMan.API.Domain.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMan.API.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAll();

        Task<Product> GetById(int id);

        Product GetByCode(string code);

        Task<Product> Create(Product entity);

        Task<Product> Update(int id, Product entity);

        Task Delete(int id);
    }
}
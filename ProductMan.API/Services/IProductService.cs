using ProductMan.API.Domain.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMan.API.Services
{
    public interface IProductService
    {
        ICollection<Product> RetrieveAllProducts();

        Task<Product> RetrieveProductById(int id);

        bool DuplicateCheckByCode(string code);

        Task<Product> Create(Product entity);

        Task<Product> Update(int id, Product entity);

        Task Delete(int id);
    }
}
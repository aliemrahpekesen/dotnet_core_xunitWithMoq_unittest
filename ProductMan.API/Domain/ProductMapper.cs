using ProductMan.API.Domain.Context.Entities;
using ProductMan.API.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMan.API.Domain
{
    public class ProductMapper
    {
        public Product ToEntity(PostProductRequest request)
        {
            return new Product()
            {
                Code = request.Code,
                Name = request.Name,
                StockCount = request.StockCount,
                TaxRate = request.TaxRate,
                UnitPrice = request.UnitPrice
            };
        }

        public Product ToEntity(int id, PutProductRequest request)
        {
            return new Product()
            {
                ProductID = id,
                Code = request.Code,
                Name = request.Name,
                StockCount = request.StockCount,
                TaxRate = request.TaxRate,
                UnitPrice = request.UnitPrice
            };
        }
    }
}
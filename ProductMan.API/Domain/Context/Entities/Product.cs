using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMan.API.Domain.Context.Entities
{
    public class Product
    {
        public Product()
        {
        }

        public Product(int productID)
        {
            this.ProductID = productID;
        }

        public int ProductID { get; set; }

        public String Code { get; set; }

        public String Name { get; set; }

        public decimal? TaxRate { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? StockCount { get; set; }
    }
}
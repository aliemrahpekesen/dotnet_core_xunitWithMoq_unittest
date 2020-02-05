using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMan.API.Domain.Model
{
    public class PostProductResponse
    {
        public int? ProductId { get; set; }

        public String Code { get; set; }

        public String Name { get; set; }

        public decimal? TaxRate { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? StockCount { get; set; }
    }
}
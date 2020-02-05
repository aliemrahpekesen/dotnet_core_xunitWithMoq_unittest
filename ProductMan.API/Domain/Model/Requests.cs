using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductMan.API.Domain.Model
{
    public class PostProductRequest
    {
        [StringLength(50)]
        public String Code { get; set; }

        [Required]
        [StringLength(200)]
        public String Name { get; set; }

        public decimal? TaxRate { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? StockCount { get; set; }
    }

    public class PutProductRequest
    {
        [Key]
        public int? ProductID { get; set; }

        [StringLength(50)]
        public String Code { get; set; }

        [Required]
        [StringLength(200)]
        public String Name { get; set; }

        public decimal? TaxRate { get; set; }

        public decimal? UnitPrice { get; set; }

        public int? StockCount { get; set; }
    }
}
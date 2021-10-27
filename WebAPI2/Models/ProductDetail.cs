using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2.Models
{
    public class ProductDetail
    {
        public int ProductDetailId { get; set; }
        public int? ProductId { get; set; }
        public string Details { get; set; }

        public Product Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWeb.Dtos
{
    public class ProductDetailDto
    {
        public int ProductDetailId { get; set; }
        public int? ProductId { get; set; }
        public string Details { get; set; }

        public virtual ProductDto Product { get; set; }
    }
}

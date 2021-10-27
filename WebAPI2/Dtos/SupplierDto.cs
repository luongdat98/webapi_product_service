using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWeb.Dtos
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string NameSupplier { get; set; }
        public string Address { get; set; }

        public virtual ICollection<ProductDto> Products { get; set; }
    }
}

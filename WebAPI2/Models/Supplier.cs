using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string NameSupplier { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

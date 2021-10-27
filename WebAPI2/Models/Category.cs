using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string NameCategory { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}

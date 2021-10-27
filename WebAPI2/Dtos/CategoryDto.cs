using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWeb.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string NameCategory { get; set; }

        public virtual ICollection<ProductDto> Products { get; set; }
    }
}

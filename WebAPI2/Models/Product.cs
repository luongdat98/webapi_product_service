using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI2.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        public int? Rating { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }

        public  Category Category { get; set; }
        public  Supplier Supplier { get; set; }
        public  ICollection<ProductDetail> ProductDetails { get; set; }
    }
}

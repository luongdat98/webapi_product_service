using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Helpers;
using WebAPI2.Models;

namespace APIWeb.Dtos
{
    public class ProductDto
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
    }

    public class ProductAddDto
    {
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        public int? Rating { get; set; }
        public decimal? Price { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
    }

    public class ProductEditDto
    {
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime? DiscontinuedDate { get; set; }
        public int? Rating { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
    }

    public class ProductIdModel
    {
        public int ProductId { get; set; }
    }

    public class ProductPaging
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }

    public class ProductDataToClientDto
    {
        public PageList<Product> Items { get; set; }
        public Object Meta { get; set; }
    }
}

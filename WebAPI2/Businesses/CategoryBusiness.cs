using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Models;
using WebAPI2.Repositories;

namespace WebAPI2.Businesses
{
    public class CategoryBusiness : ICategoryRepository
    {
        private readonly EcommerceContext _ecommerceDBContext;

        public CategoryBusiness(EcommerceContext ecommerceDBContext)
        {
            _ecommerceDBContext = ecommerceDBContext;
        }

        public IEnumerable<Category> GetCategorys()
        {
            var categories = _ecommerceDBContext.Categories.ToList().AsQueryable();
            return categories;
        }
    }
}

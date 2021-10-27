using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Models;
using WebAPI2.Repositories;

namespace WebAPI2.Businesses
{
    public class SupplierBusiness : ISupplierRepository
    {
        private readonly EcommerceContext _ecommerceDBContext;

        public SupplierBusiness(EcommerceContext ecommerceDBContext)
        {
            _ecommerceDBContext = ecommerceDBContext;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            var suppliers = _ecommerceDBContext.Suppliers.ToList().AsQueryable();
            return suppliers;
        }
    }
}

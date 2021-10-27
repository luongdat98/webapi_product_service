using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI2.Models;

namespace WebAPI2.Repositories
{
    public interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSuppliers();
    }
}

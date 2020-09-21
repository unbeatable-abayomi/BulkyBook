using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }

         ICoverTypeRepository CoverType { get; }

         IProductRepository Product { get; }
         ICompanyRepository Company { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IOrderHeaderRepository OrderHeader  { get; }

        IOrderDetailsRepository OrderDetails { get; }
        
        IShoppingCartRepository ShoppingCart { get; }
        ISP_Call SP_Call { get; }

        void Save();
    }
}

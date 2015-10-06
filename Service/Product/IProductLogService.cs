using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Product;

namespace Service.Product
{
    public partial interface IProductLogService
    {
        ProductLog insertNewProductLog(ProductLog productlog);
        IQueryable<ProductLog> GetAllProductsLogByQueryable();
        IList<ProductLog> GetAllProductsLogs();
        void deleteProductLogs(long ProductID);
    }
}

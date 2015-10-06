using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Product;

namespace Service.Product
{
    public partial interface IProductInfoService
    {
        ProductInfo insertNewProductInfo(ProductInfo productinfo);
        IQueryable<ProductInfo> GetAllProductsInfoByQueryable();
        IList<ProductInfo> GetAllProductsInfos();
        void deleteProductInfos(long ProductID);
    }
}

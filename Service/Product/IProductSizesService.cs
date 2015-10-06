using Core.Group;
using Core.Product;
using Core.Size;
using System.Collections.Generic;
using System.Linq;

namespace Service.Product 
{
    public partial interface IProductSizesService
    {
        #region ProductsSize
        ProductSizes insertNewProductSize(ProductSizes size);
        IQueryable<ProductSizes> GetAllProductsSizeByQueryable();
        IList<ProductSizes> GetAllProductsSize();
        ProductSizes updateProductsSize(ProductSizes size);
        void deleteProductSizes(long ProductID);
        #endregion 
    }
} 

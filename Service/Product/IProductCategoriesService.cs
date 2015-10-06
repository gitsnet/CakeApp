using Core.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Product
{
    public partial interface IProductCategoriesService
    {
        ProductCategories insertNewProductCategory(ProductCategories productcategory);
       IQueryable<ProductCategories> GetAllProductsCategoryByQueryable();
       IList<ProductCategories> GetAllProductsCategories();
       void deleteProductCategories(long ProductID);
    }
}

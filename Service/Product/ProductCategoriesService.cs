using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Product;
using Core.Data;

namespace Service.Product
{
    public partial class ProductCategoriesService : IProductCategoriesService
    {
        private readonly IRepository<ProductCategories> _productCategoryService;

        public ProductCategoriesService(IRepository<ProductCategories> productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        public ProductCategories insertNewProductCategory(ProductCategories productcategory)
        {
            if (productcategory != null)
            {
                _productCategoryService.Insert(productcategory);
                return productcategory;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<ProductCategories> GetAllProductsCategoryByQueryable()
        {
            return _productCategoryService.Table;
        }

        public IList<Core.Product.ProductCategories> GetAllProductsCategories()
        {
            return _productCategoryService.Table.ToList();
        }

        public void deleteProductCategories(long ProductID)
        {
            if (ProductID != null)
            {
                List<ProductCategories> result = _productCategoryService.Table.Where(a => a.ProductID == ProductID).ToList();
                if (result != null)
                {
                    foreach (ProductCategories item in result)
                    {
                        _productCategoryService.Delete(item);
                        
                    }
                }

            }
        }
    }
}

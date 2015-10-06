using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Product;

namespace Service.Product
{
    public partial class ProductInfoService : IProductInfoService
    {
         private readonly IRepository<ProductInfo> _productInfoService;

         public ProductInfoService(IRepository<ProductInfo> productInfoService)
        {
            _productInfoService = productInfoService; 
        }

        public ProductInfo insertNewProductInfo(ProductInfo productinfo)
        {
            if (productinfo != null)
            {
                _productInfoService.Insert(productinfo);
                return productinfo;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<ProductInfo> GetAllProductsInfoByQueryable()
        {
            return _productInfoService.Table;
        }

        public IList<Core.Product.ProductInfo> GetAllProductsInfos()
        {
            return _productInfoService.Table.ToList();
        }

        public void deleteProductInfos(long ProductID)
        {
            if (ProductID != null)
            {
                List<ProductInfo> result = _productInfoService.Table.Where(a => a.ProductID == ProductID).ToList();
                
                if (result != null)
                {
                    foreach (ProductInfo item in result)
                    {
                        _productInfoService.Delete(item);
                       

                    }
                }

            }
        }
    }
}

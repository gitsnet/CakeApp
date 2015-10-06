using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Product;
using Core.Data;

namespace Service.Product
{
    public partial class ProductLogService:IProductLogService
    {
        private readonly IRepository<ProductLog> _productLogService;
        public ProductLogService(IRepository<ProductLog> productLogService)
        {
            _productLogService = productLogService; 
        }

        public ProductLog insertNewProductLog(ProductLog productlog)
        {
            if (productlog != null)
            {
                _productLogService.Insert(productlog);
                return productlog;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<ProductLog> GetAllProductsLogByQueryable()
        {
            return _productLogService.Table;
        }

        public IList<ProductLog> GetAllProductsLogs()
        {
            return _productLogService.Table.ToList();
        }

        public void deleteProductLogs(long ProductID)
        {
            if (ProductID != null)
            {
                List<ProductLog> result = _productLogService.Table.Where(a => a.ProductID == ProductID).ToList();

                if (result != null)
                {
                    foreach (ProductLog item in result)
                    {
                        _productLogService.Delete(item);


                    }
                }

            }
        }
    }
}

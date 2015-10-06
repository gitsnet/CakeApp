using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Core.Product;

namespace Service.Product
{
    public partial class ProductPriceService : IProductPriceService
    {
        private readonly IRepository<ProductPrice> _productPriceService;

        public ProductPriceService(IRepository<ProductPrice> productPriceService)
        {
            _productPriceService = productPriceService; 
        }

        public ProductPrice insertNewProductPrice(ProductPrice price)
        {
            if (price != null)
            {
                _productPriceService.Insert(price);
                return price;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<ProductPrice> GetAllProductsPriceByQueryable()
        {
            return _productPriceService.Table;
        }

        public IList<ProductPrice> GetAllProductsPrice()
        {
            return _productPriceService.Table.ToList();
        }
        public void deleteProductPrices(long ProductID)
        {
            if (ProductID != null)
            {
                List<ProductPrice> result = _productPriceService.Table.Where(a => a.ProductID == ProductID).ToList();

                if (result != null)
                {
                    foreach (ProductPrice item in result)
                    {
                        _productPriceService.Delete(item);


                    }
                }

            }
        }
    }
}

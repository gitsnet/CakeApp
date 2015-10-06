using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Product;

namespace Service.Product
{
  public  partial interface IProductPriceService
    {
      ProductPrice insertNewProductPrice(ProductPrice price);
      IQueryable<ProductPrice> GetAllProductsPriceByQueryable();
      IList<ProductPrice> GetAllProductsPrice();
      void deleteProductPrices(long ProductID);
        
    }
}

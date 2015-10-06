using Core.Group;
using Core.Product;
using Core.Size;
using System.Collections.Generic;
using System.Linq;

namespace Service.Product 
{
    public partial interface IProductsService
    {
        #region Products
        Products insertNewProducts(Products product);
        IQueryable<Products> GetAllProductsByQueryable();
        IList<Products> GetAllProducts();
        Products updateProducts(Products product);
         Products deleteProduct(Products product);
        #endregion 
    }
} 

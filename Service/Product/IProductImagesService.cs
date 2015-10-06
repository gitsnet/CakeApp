using Core.Group;
using Core.Product;
using Core.Size;
using System.Collections.Generic;
using System.Linq;

namespace Service.Product 
{
    public partial interface IProductImagesService
    {
        #region ProductsImages
        ProductImages insertNewProductImages(ProductImages image); 
        IQueryable<ProductImages> GetAllProductImagesByQueryable();
        IList<ProductImages> GetAllProductImages();
        ProductImages updateProductImages(ProductImages image);
         void deleteProductImage(long ProductID);
       
        #endregion 
    }
} 

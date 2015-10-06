using Core.Data;
using Core.Product;
using Core.Size;
using Service.Category;
using System.Collections.Generic;
using System.Linq;

namespace Service.Product
{
    public partial class ProductImagesService : IProductImagesService
    {
        private readonly IRepository<ProductImages> _productImageService;
        public ProductImagesService(IRepository<ProductImages> productImageService)
        {
            _productImageService = productImageService;
        }
        public ProductImages insertNewProductImages(ProductImages image)
        {
            if (image != null)
            {
                _productImageService.Insert(image);
                return image;
            }
            else
            {
                return null;
            }
        }
        public IQueryable<ProductImages> GetAllProductImagesByQueryable()
        {
            return _productImageService.Table;
        }

        public IList<ProductImages> GetAllProductImages()
        {
            return _productImageService.Table.ToList();
        }
        public ProductImages updateProductImages(ProductImages image)
        {
            if (image != null)
            {
                _productImageService.Update(image);
                return image;
            }
            else
            {
                return null;
            }
        }
        public void deleteProductImage(long ProductID)
        {
            if (ProductID != null)
            {
                List<ProductImages> result = _productImageService.Table.Where(a => a.ProductID == ProductID).ToList();
                if (result != null)
                {
                    foreach (ProductImages item in result)
                    {

                        _productImageService.Delete(item);
                    }
                }

            }


        }

    }
}

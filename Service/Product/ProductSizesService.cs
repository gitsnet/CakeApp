using Core.Data;
using Core.Product;
using Core.Size;
using Service.Category;
using System.Collections.Generic;
using System.Linq;

namespace Service.Product
{
    public partial class ProductSizesService : IProductSizesService
    {
        private readonly IRepository<ProductSizes> _productSizeService;
        public ProductSizesService(IRepository<ProductSizes> productSizeService)
        {
            _productSizeService = productSizeService;
        }
        public ProductSizes insertNewProductSize(ProductSizes size)
        {
            if (size != null)
            {
                _productSizeService.Insert(size);
                return size;
            }
            else
            {
                return null;
            }
        }
        public IQueryable<ProductSizes> GetAllProductsSizeByQueryable()
        {
            return _productSizeService.Table;
        }

        public IList<ProductSizes> GetAllProductsSize()
        {
            return _productSizeService.Table.ToList();
        }
        public ProductSizes updateProductsSize(ProductSizes size)
        {
            if (size != null)
            {
                _productSizeService.Update(size);
                return size;
            }
            else
            {
                return null;
            }
        }

        public void deleteProductSizes(long ProductID)
        {
            if (ProductID != null)
            {
                List<ProductSizes> result = _productSizeService.Table.Where(a => a.ProductID == ProductID).ToList();

                if (result != null)
                {
                    foreach (ProductSizes item in result)
                    {
                        _productSizeService.Delete(item);

                    }
                }

            }
        }
    }
}

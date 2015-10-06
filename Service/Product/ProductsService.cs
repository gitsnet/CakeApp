using Core.Data;
using Core.Product;
using Core.Size;
using Service.Category;
using System.Collections.Generic;
using System.Linq;

namespace Service.Product
{
    public partial class ProductsService : IProductsService
    {
        private readonly IRepository<Products> _productService;
        public ProductsService(IRepository<Products> productService)
        {
            _productService = productService;
        }
        public Products insertNewProducts(Products product)
        {
            if (product != null)
            {
                _productService.Insert(product);
                return product;
            }
            else
            {
                return null;
            }
        }
        public IQueryable<Products> GetAllProductsByQueryable()
        {
            return _productService.Table;
        }

        public IList<Products> GetAllProducts()
        {
            return _productService.Table.ToList<Products>();
        }
        public Products updateProducts(Products product)
        {
            if (product != null)
            {
                Products result = _productService.GetById(product.ProductID);

                result.ProductID = product.ProductID;
                result.ProductName = product.ProductName;
                result.Title = product.Title;
                result.TagID = product.TagID;
                result.GroupID = product.GroupID;
                //result.AdditionalInfo = product.AdditionalInfo;
                result.AddedBy = product.AddedBy;
                //result.Instructions = product.Instructions;
                //result.GroupID = info.GroupID;
                result.ProductImageName = product.ProductImageName;
                result.Priority = product.Priority;
                result.Status = product.Status;
                result.statusAdditionalInfo = product.statusAdditionalInfo;
                result.statusInstructions = product.statusInstructions;

                _productService.Update(result);

                

                return result;
            }
            else
            {
                return null;
            }
        }

        public Products deleteProduct(Products product)
        {
            if (product != null)
            {
                Products result = _productService.GetById(product.ProductID);

                _productService.Delete(result);
                return product;
            }
            else
            {
                return null;
            }
        }
    }
}

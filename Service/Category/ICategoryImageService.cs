using Core.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Category
{
    public partial interface ICategoryImageService
    {
        #region CategoryImageService
        CategoryImages insertNewCategoryImages(CategoryImages categoryImages);
        IQueryable<CategoryImages> GetAllCategoryImagesByQueryable();
        IList<CategoryImages> GetAllCategoryImages();
        CategoryImages UpdateCategoryImages(CategoryImages categoryImages);
        CategoryImages DeleteCategoryImages(CategoryImages categoryImages);
        #endregion
    }
}

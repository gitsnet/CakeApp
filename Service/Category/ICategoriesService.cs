using Core.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Category
{
    public partial interface ICategoriesService
    {
        #region Categories
        Categories insertNewCategory(Categories category);
        IQueryable<Categories> GetAllCategoriesByQueryable(); 
        IList<Categories> GetAllCategories();
        Categories updateCategory(Categories category);
        Categories deleteCategory(Categories category);
        #endregion
    }
}

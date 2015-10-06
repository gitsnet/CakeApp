using Core.Category;
using Core.Data;
using Service.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Category
{
    public partial class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Categories> _categoriesService;
        public CategoriesService(IRepository<Categories> categoriesService)
        {
            _categoriesService = categoriesService;
        }
        public Categories insertNewCategory(Categories category)
        {
            if (category != null)
            {
                _categoriesService.Insert(category);
                return category;
            }
            else
            {
                return null;
            }
        }
        public IQueryable<Categories> GetAllCategoriesByQueryable()
        {
            return _categoriesService.Table;
        }

        public IList<Categories> GetAllCategories()
        {
            return _categoriesService.Table.ToList();
        }
        public Categories updateCategory(Categories category)
        {
            if (category != null)
            {
                Categories result = _categoriesService.GetById(category.CategoryID);
                result.CategoryID = category.CategoryID;
                result.CategoryName = category.CategoryName;
                result.Title = category.Title;
                result.TagID = category.TagID;
                result.Priority = category.Priority;
                result.Status = category.Status;

                  _categoriesService.Update(result);

                return result;
            }
            else
            {
                return null;
            }
        }

        public Categories deleteCategory(Categories category)
        {
            if (category != null)
            {
                Categories result = _categoriesService.GetById(category.CategoryID);
            
            _categoriesService.Delete(result);
                return category;
            }
            else
            {
                return null;
            }
 
        }
    }
}

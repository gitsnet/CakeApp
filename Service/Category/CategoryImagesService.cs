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
    public partial class CategoryImageService : ICategoryImageService
    {
        private readonly IRepository<CategoryImages> _categoriesImgService;
        public CategoryImageService(IRepository<CategoryImages> categoriesService)
        {
            _categoriesImgService = categoriesService;
        }
        public CategoryImages insertNewCategoryImages(CategoryImages category)
        {
            if (category != null)
            {
                _categoriesImgService.Insert(category);
                return category;
            }
            else
            {
                return null;
            }
        }
        public IQueryable<CategoryImages> GetAllCategoryImagesByQueryable()
        {
            return _categoriesImgService.Table;
        }

        public IList<CategoryImages> GetAllCategoryImages()
        {
            return _categoriesImgService.Table.ToList();
        }
        public CategoryImages UpdateCategoryImages(CategoryImages category)
        {
            if (category != null)
            {
                CategoryImages result = _categoriesImgService.GetById(category.CategoryImageID);
                //result.CategoryID = category.CategoryID;
                result.Title = category.Title;
                result.ImageName = category.ImageName;
                result.ImageAlt = category.ImageAlt;
                result.Priority = category.Priority;
                result.Status = category.Status;

                  _categoriesImgService.Update(result);

                return result;
            }
            else
            {
                return null;
            }
        }

        public CategoryImages DeleteCategoryImages(CategoryImages category)
        {
            if (category != null)
            {
                CategoryImages result = _categoriesImgService.GetById(category.CategoryImageID);
            
            _categoriesImgService.Delete(result);
                return category;
            }
            else
            {
                return null;
            }
 
        }
    }
}

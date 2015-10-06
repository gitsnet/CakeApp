using CakeApp.Areas.Admin.Models.Category;
using Core.Category;
using Core.Tag;
using Service.Category;
using Service.Tag;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CakeApp.Areas.Admin.Controllers.Category
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService _categoryService;
        private readonly ITagService _tagService;
        private readonly ICategoryImageService _categoryImageService;
        public CategoriesController(ICategoriesService categoryService, ITagService tagService, ICategoryImageService categoryImageService)
        {
            _categoryService = categoryService;
            _tagService = tagService;
            _categoryImageService = categoryImageService;
        }
        public ActionResult CategoryList()
        {
            List<CategoriesModel> model = new List<CategoriesModel>();
            try
            {
                //IList<Categories> categoryList = _categoryService.GetAllCategories().OrderByDescending(t => t.Priority).ToList();

                IList<Categories> categoryList = _categoryService.GetAllCategories().OrderBy(t => t.Priority).ToList();

                if (categoryList.Count > 0)
                {
                    foreach (var cat in categoryList)
                    {

                        model.Add(new CategoriesModel
                        {
                            CategoryID = cat.CategoryID,
                            CategoryName = cat.CategoryName,
                            Title = cat.Title,
                            TagID = cat.TagID.ToString(),
                            TagName = cat.Tags.TagName,
                            Status = cat.Status ? "Enable" : "Disable",
                            Priority = cat.Priority,

                        });
                    }
                }
                View(model);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }

            return View(model);
        }

        public ActionResult AddCategory()
        {
            CategoriesModel model = new CategoriesModel();
            IList<Tags> TagList = _tagService.GetAllTags().Where(t => t.TagName != null).ToList();
            IList<CategoryImages> CategoryImagesList = _categoryImageService.GetAllCategoryImages().ToList();

            //var maxPriority = _categoryService.GetAllCategoriesByQueryable().Max(t => t.Priority);

            //if (maxPriority == null || maxPriority == 0)
            //    model.Priority = 10;
            //else
            //    model.Priority = maxPriority + 10;

            model.Priority = 0;


            if (TagList.Count > 0)
            {
                foreach (var item in TagList)
                {
                    model.TagList.Add(new Tags
                    {
                        TagID = item.TagID,
                        TagName = item.TagName
                    });
                }

            }
            if (CategoryImagesList.Count > 0)
            {
                foreach (var item in CategoryImagesList)
                {
                    model.CategoryImagesList.Add(new CategoryImages
                    {
                        CategoryImageID = item.CategoryImageID,
                        ImageName = item.ImageName
                    });
                }

            }

            return View(model);
        }
        [HttpPost]
        public ActionResult AddCategory(CategoriesModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var a = TempData["Tags"];
                    var categoryExists = _categoryService.GetAllCategoriesByQueryable();
                    if (categoryExists != null)
                    {
                        if (categoryExists.Where(t => t.CategoryName == model.CategoryName).Count() == 0)
                        {
                            if (categoryExists.Where(t => t.Priority == model.Priority).Count() == 0)
                            {
                                if (Request.Files.Count > 0)
                                {
                                    var Idfile = Request.Files[0];
                                    if (Idfile != null && Idfile.ContentLength > 0)
                                    {
                                        var filename = Path.GetFileName(Idfile.FileName);
                                        string extension = Path.GetExtension(Idfile.FileName);
                                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".bmp" || extension.ToLower() == ".png" || extension.ToLower() == ".gif")
                                        {
                                            string fName = DateTime.Now.ToString("yyyyMMdd_hhssff") + extension;
                                            var path = Path.Combine(Server.MapPath("~/Content/Assets/CategoryImages/"), fName);
                                            Idfile.SaveAs(path);



                                            Categories category = new Categories();
                                            category.CategoryName = model.CategoryName;
                                            category.Title = model.Title;
                                            category.TagID = Convert.ToInt64(model.TagID);
                                            category.Priority = model.Priority;
                                            //category.CategoryImageID = model.CategoryImageID;
                                            //category.CategoryImageName = model.ImageName;
                                            category.CategoryImageName = fName;
                                            category.AddedBy = 2;
                                            category.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                            _categoryService.insertNewCategory(category);
                                            model.CategoryID = category.CategoryID;
                                            model.ImageName = fName;


                                            return RedirectToAction("CategoryList", "Categories", new { @area = "Admin" });

                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "Choose a valid image type file.");
                                        }
                                    }
                                    else
                                    {
                                        //ModelState.AddModelError("", "Choose an image file.");

                                        Categories category = new Categories();
                                        category.CategoryName = model.CategoryName;
                                        category.Title = model.Title;
                                        category.TagID = Convert.ToInt64(model.TagID);
                                        category.Priority = model.Priority;
                                        //category.CategoryImageID = model.CategoryImageID;
                                        //category.CategoryImageName = model.ImageName;
                                        category.CategoryImageName = null;
                                        category.AddedBy = 2;
                                        category.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                        _categoryService.insertNewCategory(category);
                                        model.CategoryID = category.CategoryID;
                                        model.ImageName = null;

                                        return RedirectToAction("CategoryList", "Categories", new { @area = "Admin" });
                                    }
                                }
                                else
                                {
                                    //ModelState.AddModelError("", "Choose an image file.");

                                    Categories category = new Categories();
                                    category.CategoryName = model.CategoryName;
                                    category.Title = model.Title;
                                    category.TagID = Convert.ToInt64(model.TagID);
                                    category.Priority = model.Priority;
                                    //category.CategoryImageID = model.CategoryImageID;
                                    //category.CategoryImageName = model.ImageName;
                                    category.CategoryImageName = null;
                                    category.AddedBy = 2;
                                    category.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                    _categoryService.insertNewCategory(category);
                                    model.CategoryID = category.CategoryID;
                                    model.ImageName = null;


                                    return RedirectToAction("CategoryList", "Categories", new { @area = "Admin" });
                                }

                            }
                            else
                            {
                                ModelState.AddModelError("", "Priority value already exists");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", model.CategoryName + " already exists");
                        }
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Not inserted");
                }
               

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            IList<Tags> TagList = _tagService.GetAllTags().Where(t => t.TagName != null).ToList();
            var maxPriority = _categoryService.GetAllCategoriesByQueryable().Max(t => t.Priority);
            if (maxPriority == null || maxPriority == 0)
                model.Priority = 10;
            else
                model.Priority = maxPriority + 10;
            if (TagList.Count > 0)
            {
                foreach (var item in TagList)
                {
                    model.TagList.Add(new Tags
                    {
                        TagID = item.TagID,
                        TagName = item.TagName
                    });
                }

            }
            IList<CategoryImages> CategoryImagesList = _categoryImageService.GetAllCategoryImages().ToList();
            if (CategoryImagesList.Count > 0)
            {
                foreach (var item in CategoryImagesList)
                {
                    model.CategoryImagesList.Add(new CategoryImages
                    {
                        CategoryImageID = item.CategoryImageID,
                        ImageName = item.ImageName
                    });
                }

            }
            return View(model);

        }


        [HttpPost]
        public ActionResult DeleteCategory(long id)
        {
            try
            {
                Categories categoryExists = _categoryService.GetAllCategoriesByQueryable().Where(t => t.CategoryID == id).FirstOrDefault();
                if (categoryExists != null)
                {
                    _categoryService.deleteCategory(categoryExists);
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Not found", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public ActionResult EditCategories(CategoriesModel updateCategoryModel)
        {
            try
            {

                if (updateCategoryModel.CategoryID > 0)
                {
                    Categories category = new Categories();
                    category.CategoryID = updateCategoryModel.CategoryID;
                    category.CategoryName = updateCategoryModel.CategoryName;
                    category.Title = updateCategoryModel.Title;
                    category.TagID = Convert.ToInt64(updateCategoryModel.TagID);
                    category.Priority = updateCategoryModel.Priority;
                    category.Status = Convert.ToBoolean(updateCategoryModel.Status == "Enable" ? "true" : "false");

                    _categoryService.updateCategory(category);

                    return Json("true", JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json("Not Updated", JsonRequestBehavior.AllowGet);

                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);

            }


        }

        public ActionResult CategoryImage()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetTagList()
        {

            try
            {

                IList<Tags> TagList = _tagService.GetAllTags().Where(t => t.TagName != null).ToList();
                List<Tags> TagsLi = new List<Tags>();
                if (TagList.Count > 0)
                {
                    foreach (var item in TagList)
                    {
                        Tags tags = new Tags();
                        tags.TagID = item.TagID;
                        tags.TagName = item.TagName;
                        TagsLi.Add(tags);

                    }


                }

                else
                {

                    ModelState.AddModelError("", "Error");

                }

                return Json(TagsLi, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();


            }
        }

        public class UpdateCategoryModel
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public string Title { get; set; }
            public int TagID { get; set; }
            public string Image { get; set; }
            public int Priority { get; set; }
            public bool Status { get; set; }

        }

    }
}

using Service.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CakeApp.Areas.Admin.Models.Category;
using System.IO;
using Core.Category;
using CakeApp.Common;

namespace CakeApp.Areas.Admin.Controllers.Category
{
    public class CategoryImageController : BaseController
    {
        private readonly ICategoryImageService _categoryImageService;

        public CategoryImageController(ICategoryImageService categoryImageService)
        {
            _categoryImageService = categoryImageService;
        }
        //
        // GET: /Admin/CategoryImage/
        public ActionResult CategoryImageList()
        {

            List<CategoryImagesModel> modelList = new List<CategoryImagesModel>();
            try
            {
                IList<CategoryImages> categoryimgList = _categoryImageService.GetAllCategoryImages().OrderBy(t => t.Priority).ToList();

                //IList<CategoryImages> categoryimgList = _categoryImageService.GetAllCategoryImages().OrderByDescending(t => t.Priority).ToList();

                if (categoryimgList.Count > 0)
                {
                    foreach (var cat in categoryimgList)
                    {

                        modelList.Add(new CategoryImagesModel
                        {
                            CategoryImageID = cat.CategoryImageID,
                            ImageName = cat.ImageName,
                            Title = cat.Title,
                            ImageAlt = cat.ImageAlt,
                            //CategoryImages = cat,
                            Status = cat.Status ? "Enable" : "Disable",
                            Priority = cat.Priority,

                        });
                    }
                }
                View(modelList);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }
            return View(modelList);
        }
        public ActionResult AddCategoryImage()
        {
            CategoryImagesModel model = new CategoryImagesModel();

            var maxPriority = _categoryImageService.GetAllCategoryImagesByQueryable().Max(t => t.Priority);
            if (maxPriority == null || maxPriority == 0)
            {
                model.Priority = 10;
            }
            else
            {
                model.Priority = maxPriority + 10;

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCategoryImage(CategoryImagesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var categoryImgExists = _categoryImageService.GetAllCategoryImagesByQueryable();

                    if (categoryImgExists != null)
                    {
                        string imgStr = CommonMethods.RemoveSpecialCharacters(model.Title);
                        if (categoryImgExists.Where(t => t.Title == model.Title).Count() == 0)
                        {
                            if (categoryImgExists.Where(t => t.Priority == model.Priority).Count() == 0)
                            {
                                if (Request.Files.Count > 0)
                                {
                                    var Idfile = Request.Files[0];
                                    if (Idfile != null && Idfile.ContentLength > 0)
                                    {
                                        var filename = Path.GetFileName(Idfile.FileName);
                                        string extension = Path.GetExtension(Idfile.FileName);
                                        if (extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".bmp" || extension.ToLower() == ".gif" || extension.ToLower() == ".jpeg")
                                        {
                                            //string fName = DateTime.Now.ToString("yyyyMMdd_hhssff") + extension;
                                            string fName = imgStr.ToLower() + extension;
                                            var path = Path.Combine(Server.MapPath("~/Content/Assets/CategoryImages/"), fName);
                                            Idfile.SaveAs(path);
                                            CategoryImages categoryimages = new CategoryImages();
                                            categoryimages.ImageName = imgStr.ToLower()+extension;
                                            categoryimages.Title = model.Title;
                                            categoryimages.ImageAlt = model.ImageAlt;
                                            categoryimages.Priority = Convert.ToInt32(model.Priority);
                                            categoryimages.Status = Convert.ToBoolean(model.Status == "1" ? true : false);

                                            _categoryImageService.insertNewCategoryImages(categoryimages);
                                            model.CategoryImageID = categoryimages.CategoryImageID;

                                            return RedirectToAction("CategoryImageList", "CategoryImage", new { @area = "Admin" });
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "Choose a valid image type file.");
                                        }
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "File is Corrupt.");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Choose a image file.");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "Priority value already exists");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", model.Title + " already exists");
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
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCategoryImage(CategoryImagesModel updateCategoryImgModel)
        {
            try
            {

                if (updateCategoryImgModel.CategoryImageID > 0)
                {
                    CategoryImages categoryimg = new CategoryImages();
                    categoryimg.CategoryImageID = updateCategoryImgModel.CategoryImageID;
                    categoryimg.ImageName = updateCategoryImgModel.ImageName.ToLower();
                    categoryimg.Title = updateCategoryImgModel.Title;
                    categoryimg.ImageAlt = updateCategoryImgModel.ImageAlt;
                    categoryimg.Priority =Convert.ToInt32( updateCategoryImgModel.Priority);
                    categoryimg.Status = Convert.ToBoolean(updateCategoryImgModel.Status == "Enable" ? "true" : "false");

                    _categoryImageService.UpdateCategoryImages(categoryimg);

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

        [HttpPost]
        public ActionResult DeleteImgCategory(long id)
        {
            try
            {
                CategoryImages categoryimgExists = _categoryImageService.GetAllCategoryImagesByQueryable().Where(t => t.CategoryImageID==id).FirstOrDefault();
                if (categoryimgExists != null)
                {
                    _categoryImageService.DeleteCategoryImages(categoryimgExists);
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
    }
}
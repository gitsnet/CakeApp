using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using System.Net.Http;
//using System.Web.Http;
using System.Web.Mvc;
using CakeApp.Areas.Admin.Models.Attributes;
using Service.Tag;
using Core.Tag;

namespace CakeApp.Areas.Admin.Controllers.Attributes
{
    public class TagController : BaseController
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;

        }
        public ActionResult List()
        {
            List<TagModel> model = new List<TagModel>();
            try
            {
                IList<Tags> tagList = _tagService.GetAllTags().ToList();

                if (tagList.Count > 0)
                {
                    foreach (var tag in tagList)
                    {

                        model.Add(new TagModel
                        {
                            TagID = tag.TagID,
                            TagName = tag.TagName,
                            Title = tag.Title,
                           
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

        public ActionResult AddTag()
        {
            TagModel model = new TagModel();

            return View(model);
        }
        [HttpPost]
        public ActionResult AddTag(TagModel model)
        {
            
            try
            {
                if (ModelState.IsValid)
                {

                    var tagExists = _tagService.GetAllTagsByQueryable();
                    if (tagExists != null)
                    {
                        if (tagExists.Where(t => t.TagName == model.TagName).Count() == 0)
                        {
                            //if (tagExists.Where(t => t.Title == model.Title).Count() == 0)
                            //{
                                Tags tag = new Tags();
                                tag.TagName = model.TagName;
                                tag.Title = model.Title;

                                _tagService.insertNewTag(tag);
                                model.TagID = tag.TagID;

                                return RedirectToAction("List", "Tag", new { @area = "Admin" });

                            //}
                            //else
                            //{
                            //    ModelState.AddModelError("", "Tag Title already exists");
                            //}
                        }
                        else
                        {
                            ModelState.AddModelError("", model.TagName + " already exists");
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
        public ActionResult DeleteTag(long id)
        {
            try
            {
                Tags tagExists = _tagService.GetAllTagsByQueryable().Where(t => t.TagID==id).FirstOrDefault();
                if (tagExists != null)
                {
                    _tagService.deleteTag(tagExists);
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
        public ActionResult EditTags(TagModel updateTagModel)
        {
            try
            {

                if (updateTagModel.TagID > 0)
                {
                    Tags tag = new Tags();
                    tag.TagID = updateTagModel.TagID;
                    tag.TagName = updateTagModel.TagName;
                    tag.Title = updateTagModel.Title;

                    _tagService.updateTag(tag);

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


    }
}

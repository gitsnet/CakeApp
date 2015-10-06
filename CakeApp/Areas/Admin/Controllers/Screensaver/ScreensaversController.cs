using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Screensaver;
using CakeApp.Areas.Admin.Models.Screensaver;
using System.IO;
using Core.Screensaver;

namespace CakeApp.Areas.Admin.Controllers.Screensaver
{
    public class ScreensaversController : BaseController
    {
        private readonly IScreensaverService _screensaverService;
        public ScreensaversController(IScreensaverService screensaverService)
        {

            _screensaverService = screensaverService;

        }
        public ActionResult ScreensaverList()
        {
            try
            {
                IList<Sreensavers> screensaverList = _screensaverService.GetAllSreensavers().OrderByDescending(t => t.Priority).ToList();
                List<ScreensaversModel> model = new List<ScreensaversModel>();
                if (screensaverList.Count > 0)
                {
                    foreach (var screensaver in screensaverList)
                    {

                        model.Add(new ScreensaversModel
                        {
                            ScreensaverID = screensaver.ScreensaverID,
                            Name = screensaver.Name,
                            Title = screensaver.Title,
                            Priority = screensaver.Priority,
                            //Status = screensaver.Status ? "Active" : "Inactive",
                            Status = (screensaver.Status) == true ? "Enable" : "Disable",
                            Video = screensaver.Video

                        });
                    }
                }
                View(model);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }
        public ActionResult AddScreensaver()
        {
            ScreensaversModel model = new ScreensaversModel();
            var maxPriority = _screensaverService.GetAllSreensaversQueryable().Max(t => t.Priority);
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
        public ActionResult AddScreensaver(ScreensaversModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var screensaverExists = _screensaverService.GetAllSreensaversQueryable();
                    if (screensaverExists != null)
                    {
                        if (screensaverExists.Where(t => t.Name == model.Name).Count() == 0)
                        {
                            if (screensaverExists.Where(t => t.Priority == model.Priority).Count() == 0)
                            {
                                if (Request.Files.Count > 0)
                                {
                                    var Idfile = Request.Files[0];
                                    if (Idfile != null && Idfile.ContentLength > 0)
                                    {
                                        var filename = Path.GetFileName(Idfile.FileName);
                                        string extension = Path.GetExtension(Idfile.FileName);
                                        if (extension.ToLower() == ".webm" || extension.ToLower() == ".flv" || extension.ToLower() == ".mkv" || extension.ToLower() == ".mp4" || extension.ToLower() == ".avi" || extension.ToLower() == ".mpg" || extension.ToLower() == ".mov" || extension.ToLower() == ".wmv")
                                        {
                                            string fName = DateTime.Now.ToString("yyyyMMdd_hhssff") + extension;
                                            var path = Path.Combine(Server.MapPath("~/Content/Assets/ScreensaverVideos/"), fName);
                                            Idfile.SaveAs(path);
                                            Sreensavers sreensavers = new Sreensavers();
                                            sreensavers.Name = model.Name;
                                            sreensavers.Title = model.Title;
                                            sreensavers.Priority = model.Priority;
                                            sreensavers.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                            sreensavers.Video = fName;

                                            _screensaverService.insertNewSreensaver(sreensavers);
                                            model.ScreensaverID = sreensavers.ScreensaverID;

                                            return RedirectToAction("ScreensaverList", "Screensavers", new { @area = "Admin" });
                                        }
                                        else
                                        {
                                            ModelState.AddModelError("", "Choose a valid video type file.");
                                        }
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "File is Corrupt.");
                                    }
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Choose a viseo file.");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "Priority value already exists");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", model.Name + " already exists");
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


        public ActionResult EditScreensaver(long Id)
        {
            ScreensaversModel model = new ScreensaversModel();
            var details = _screensaverService.GetAllSreensaversQueryable().Where(s => s.ScreensaverID == Id).FirstOrDefault();
            if (details != null)
            {
                model.ScreensaverID = Id;
                model.Name = details.Name;
                model.Title = details.Title;
                model.Priority = details.Priority;
                model.Status = Convert.ToString(details.Status);
                model.Video = details.Video;

            }

            return View(model);
        }
        [HttpPost]
        public ActionResult EditScreensaver(ScreensaversModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Sreensavers sreensavers = new Sreensavers();
                    var screensaverExists = _screensaverService.GetAllSreensaversQueryable().Where(u => u.ScreensaverID != model.ScreensaverID);
                    if (screensaverExists != null)
                    {
                        if (screensaverExists.Where(t => t.Name == model.Name).Count() == 0)
                        {
                            if (screensaverExists.Where(t => t.Priority == model.Priority).Count() == 0)
                            {

                                var Idfile = Request.Files[0];
                                if (Idfile != null && Idfile.ContentLength > 0)
                                {
                                    var filename = Path.GetFileName(Idfile.FileName);
                                    string extension = Path.GetExtension(Idfile.FileName);
                                    if (extension.ToLower() == ".webm" || extension.ToLower() == ".flv" || extension.ToLower() == ".mkv" || extension.ToLower() == ".mp4" || extension.ToLower() == ".avi" || extension.ToLower() == ".mpg" || extension.ToLower() == ".mov" || extension.ToLower() == ".wmv")
                                    {
                                        string fName = DateTime.Now.ToString("yyyyMMdd_hhssff") + extension;
                                        var path = Path.Combine(Server.MapPath("~/Content/Assets/ScreensaverVideos/"), fName);
                                        Idfile.SaveAs(path);
                                        sreensavers.Video = fName;
                                      
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("", "Choose a valid video type file.");
                                    }
                                }
                                else
                                {
                                    sreensavers.Video = model.Video;
                                }

                                sreensavers.ScreensaverID = model.ScreensaverID;
                                sreensavers.Name = model.Name;
                                sreensavers.Title = model.Title;
                                sreensavers.Priority = model.Priority;
                                sreensavers.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                _screensaverService.updateSreensaver(sreensavers);

                                return RedirectToAction("ScreensaverList", "Screensavers", new { @area = "Admin" });


                            }
                            else
                            {
                                ModelState.AddModelError("", "Priority value already exists");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", model.Name + " already exists");
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
        public ActionResult DeleteSreensaver(long id)
        {
            try
            {
                Sreensavers screensaverExists = _screensaverService.GetAllSreensaversQueryable().Where(t => t.ScreensaverID == id).FirstOrDefault();
                if (screensaverExists != null)
                {
                    _screensaverService.deleteSreensaver(screensaverExists);
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
        public ActionResult GetEnableDisable(string id)
        {
            try
            {
                Sreensavers screensaverExists = _screensaverService.GetAllSreensaversQueryable().Where(t =>t.Status.ToString() == id).FirstOrDefault();
                if (screensaverExists != null)
                {

                    return Json("screensaverExists", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("screensaverExists", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
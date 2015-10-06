using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using System.Net.Http;
//using System.Web.Http;
using System.Web.Mvc;
using CakeApp.Areas.Admin.Models.Attributes;
using Core.Group;
using Service.Size;
using Service.Group;
using Core.Size;

namespace CakeApp.Areas.Admin.Controllers.Attributes
{
    public class SizeController : BaseController
    {
        private readonly ISizeService _sizeService;
        private readonly IGroupService _groupService;
        private readonly ISizeGroupService _sizeGroupService;

        public SizeController(
            ISizeService sizeService,
            IGroupService groupService,
            ISizeGroupService sizeGroupService
            )
        {
            _sizeService = sizeService;
            _groupService = groupService;
            _sizeGroupService = sizeGroupService;

        }
        public ActionResult List()
        {

            List<SizeModel> model = new List<SizeModel>();
            try
            {
                //IList<Sizes> sizeList = _sizeService.GetAllSizes().OrderByDescending(t => t.Priority).ToList();

                IList<Sizes> sizeList = _sizeService.GetAllSizes().OrderBy(t => t.Priority).ToList();


                if (sizeList.Count > 0)
                {
                    foreach (var size in sizeList)
                    {
                        string groupname = null;

                        IList<SizeGroup> sizegroupList = _sizeGroupService.GetAllSizeGroupsByQueryable().Where(t => t.SizeID == size.SizeID).ToList();

                        if (sizegroupList.Count > 0)
                        {
                            foreach (var item in sizegroupList)
                            {

                                Groups group = new Groups();
                                group.GroupID = Convert.ToInt64(item.GroupID);
                                group.GroupName = item.Groups.GroupName;
                                groupname += group.GroupName + ',';

                            }

                            groupname = groupname.Trim(',');
                        }
                        else
                        {
                            groupname = "";

                        }

                        model.Add(new SizeModel
                        {
                            SizeID = size.SizeID,
                            Size = size.Size,
                            Title = size.Title,
                            GroupNames = groupname,
                            Status = (size.Status) == true ? "Enable" : "Disable",
                            Priority = size.Priority,
                            //Price = Convert.ToDecimal(size.Price),

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
      
        public ActionResult AddSize()
        {

            SizeModel model = new SizeModel();
            IList<Groups> GroupList = _groupService.GetAllGroups().Where(t => t.GroupName != null).ToList();


            //var maxPriority = _sizeService.GetAllSizesByQueryable().Max(t => t.Priority);
            //if (maxPriority == null || maxPriority == 0)
            //    model.Priority = 10;
            //else
            //    model.Priority = maxPriority + 10;


            model.Priority = 0;

            if (GroupList.Count > 0)
            {
                foreach (var item in GroupList)
                {
                    model.GroupList.Add(new Groups
                    {
                        GroupID = item.GroupID,
                        GroupName = item.GroupName
                    });
                }

            }


            return View(model);
        }
        
        [HttpPost]
        public ActionResult AddSize(SizeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var sizeExists = _sizeService.GetAllSizesByQueryable();
                    if (sizeExists != null)
                    {
                        if (sizeExists.Where(t => t.Size == model.Size).Count() == 0)
                        {
                            if (sizeExists.Where(t => t.Priority == model.Priority).Count() == 0)
                            {
                                Sizes size = new Sizes();
                                size.Size = model.Size;
                                size.Title = model.Title;
                                //size.Price = model.Price.ToString();
                                size.Priority = model.Priority;
                                size.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                _sizeService.insertNewSize(size);
                                model.SizeID = size.SizeID;

                                if (model.SizeID > 0)
                                {
                                    string groups = model.GroupIDs;
                                    groups = groups.Trim(',');
                                    string[] strgroups = groups.Split(',');


                                    foreach (var item in strgroups)
                                    {
                                        SizeGroup sizegroup = new SizeGroup();
                                        sizegroup.SizeID = size.SizeID;
                                        sizegroup.GroupID = Convert.ToInt32(item);

                                        _sizeGroupService.insertNewSizeGroup(sizegroup);


                                    }

                                }
                                return RedirectToAction("List", "Size", new { @area = "Admin" });

                            }
                            else
                            {
                                ModelState.AddModelError("", "Priority value already exists");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", model.Size + " already exists");
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

            IList<Groups> GroupList = _groupService.GetAllGroups().Where(g => g.GroupName != null).ToList();
            //var maxPriority = _categoryService.GetAllCategoriesByQueryable().Max(t => t.Priority);
            //if (maxPriority == null || maxPriority == 0)
            //    model.Priority = 10;
            //else
            //    model.Priority = maxPriority + 10;
            if (GroupList.Count > 0)
            {
                foreach (var item in GroupList)
                {
                    model.GroupList.Add(new Groups
                    {
                        GroupID = item.GroupID,
                        GroupName = item.GroupName
                    });
                }

            }
            return View(model);

        }
        [HttpGet]
        public ActionResult EditSize(string id)
        {
            SizeModel model = new SizeModel();
            long ID = Convert.ToInt64(id);
            string groupname = null;
            string groupids = null;

           var varEditSize = _sizeService.GetAllSizesByQueryable().Where(t => t.SizeID == ID).FirstOrDefault();

           if (varEditSize != null)
            {
                model.SizeID = varEditSize.SizeID;
                model.Size = varEditSize.Size;
                model.Title = varEditSize.Title;
                model.Priority = varEditSize.Priority;
                //model.Price = Convert.ToDecimal(varEditSize.Price);

                if (varEditSize.Status == true)
                    model.Status = "1";
                else
                    model.Status = "0";


            }
            if (model.SizeID > 0)
            {

                List<SizeGroup> sizegroupList = _sizeGroupService.GetAllSizeGroupsByQueryable().Where(t => t.SizeID==ID).ToList();
               
                model.SizeGroupList = sizegroupList;
               


            }
            IList<Groups> GroupList = _groupService.GetAllGroups().Where(t => t.GroupName != null).ToList();

            if (GroupList.Count > 0)
            {
                foreach (var item in GroupList)
                {
                    model.GroupList.Add(new Groups
                    {
                        GroupID = item.GroupID,
                        GroupName = item.GroupName
                    });
                }

            }
            model.GroupNames = groupname;


            return View(model);
        }

        [HttpPost]
        public ActionResult EditSize(SizeModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model != null)
                    {
                        Sizes size = new Sizes();
                        var varEditSize = _sizeService.GetAllSizesByQueryable().Where(t => t.SizeID == model.SizeID);

                        if (varEditSize != null)
                        {
                            if (varEditSize.Where(t => t.SizeID == model.SizeID).Count() != 0)
                            {
                                size.SizeID = model.SizeID;
                                size.Size = model.Size;
                                size.Title = model.Title;
                                size.Priority = model.Priority;
                                //size.Price = model.Price.ToString();
                                size.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                _sizeService.updateSize(size);
                                
                                if (model.SizeID > 0)
                                {

                                    List<SizeGroup> sizegroupList = _sizeGroupService.GetAllSizeGroupsByQueryable().Where(t => t.SizeID == model.SizeID).ToList();

                                    model.SizeGroupList = sizegroupList;
                                    string groups = model.GroupIDs;
                                    groups = groups.Trim(',');
                                    string[] strgroups = groups.Split(',');
                                    _sizeGroupService.deleteSizeGroup(model.SizeID);
                                    
                                    foreach (var item in strgroups)
                                   {
                                       SizeGroup sizegroup = new SizeGroup();
                                       sizegroup.SizeID = size.SizeID;
                                       sizegroup.GroupID = Convert.ToInt32(item);
                                       _sizeGroupService.insertNewSizeGroup(sizegroup);
                                        
                                    }

                                }

                                return RedirectToAction("List", "Size", new { @area = "Admin" });
                            }
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Not updated");
                    }

                    IList<Groups> GroupList = _groupService.GetAllGroups().Where(t => t.GroupName != null).ToList();

                    if (GroupList.Count > 0)
                    {
                        foreach (var item in GroupList)
                        {
                            model.GroupList.Add(new Groups
                            {
                                GroupID = item.GroupID,
                                GroupName = item.GroupName
                            });
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
        public ActionResult DeleteSize(long id)
        {
            try
            {
                List<SizeGroup> sizegroupExists = _sizeGroupService.GetAllSizeGroupsByQueryable().Where(t => t.SizeID == id).ToList();
                Sizes sizeExists = _sizeService.GetAllSizesByQueryable().Where(t => t.SizeID == id).FirstOrDefault();

                if (sizeExists != null)
                {
                    if (sizegroupExists.Count > 0)
                    {
                        //foreach (var item in infogroupExists)
                        //{
                        _sizeGroupService.deleteSizeGroup(id);
                        
                        //}

                        _sizeService.deleteSize(sizeExists);
                       
                        return Json("true", JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        _sizeService.deleteSize(sizeExists);
                        
                        return Json("true", JsonRequestBehavior.AllowGet);
                    }
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

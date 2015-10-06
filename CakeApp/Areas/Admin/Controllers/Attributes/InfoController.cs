using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CakeApp.Areas.Admin.Models.Attributes;
using Service.Group;
using Service.infos;
using Core.Infos;
using Core.Group;
using Service.Infos;

namespace CakeApp.Areas.Admin.Controllers.Attributes
{
    public class InfoController : BaseController
    {
        private readonly IInfosService _infosService;
        private readonly IGroupService _groupService;
        private readonly IInfoGroupsService _infoGroupsService;
        public InfoController(
            IInfosService infosService,
            IGroupService groupService,
            IInfoGroupsService infoGroupsService
            )
        {
            _infosService = infosService;
            _groupService = groupService;
            _infoGroupsService = infoGroupsService;

        }
        public ActionResult List()
        {
            List<InfoModel> model = new List<InfoModel>();
            try
            {
                //IList<Info> infoList = _infosService.GetAllInfos().OrderByDescending(t => t.Priority).ToList();
                IList<Info> infoList = _infosService.GetAllInfos().OrderBy(t => t.Priority).ToList();



                if (infoList.Count > 0)
                {
                    foreach (var info in infoList)
                    {
                        string groupname = null;

                        IList<InfoGroups> infogroupList = _infoGroupsService.GetAllInfoGroupsByQueryable().Where(t => t.InfoID == info.InfoID).ToList();
                        if (infogroupList.Count > 0)
                        {
                            foreach (var item in infogroupList)
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
                       
                        model.Add(new InfoModel
                        {
                            InfoID = info.InfoID,
                            InfoName = info.InfoName,
                            Title = info.Title,
                            GroupNames = groupname,
                            Status = (info.Status) == true ? "Enable" : "Disable",
                            Priority = info.Priority,



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

        public ActionResult AddInfo()
        {
            InfoModel model = new InfoModel();
            IList<Groups> GroupList = _groupService.GetAllGroups().Where(t => t.GroupName != null).ToList();

            model.Priority = 0;

            //var maxPriority = _infosService.GetAllInfosByQueryable().Max(t => t.Priority);
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
        [HttpPost]
        public ActionResult AddInfo(InfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var infoExists = _infosService.GetAllInfosByQueryable();
                    if (infoExists != null)
                    {
                        if (infoExists.Where(t => t.InfoName == model.InfoName).Count() == 0)
                        {
                            if (infoExists.Where(t => t.Priority == model.Priority).Count() == 0)
                            {
                                Info info = new Info();

                                info.InfoName = model.InfoName;
                                info.Title = model.Title;

                                info.Priority = model.Priority;
                                info.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                _infosService.insertNewInfo(info);
                                model.InfoID = info.InfoID;

                                if (model.InfoID > 0)
                                {
                                    string groups = model.GroupIDs;
                                    groups = groups.Trim(',');
                                    string[] strgroups = groups.Split(',');


                                    foreach (var item in strgroups)
                                    {
                                        InfoGroups infogroup = new InfoGroups();
                                        infogroup.InfoID = info.InfoID;
                                        infogroup.GroupID = Convert.ToInt32(item);

                                        _infoGroupsService.insertNewInfoGroups(infogroup);

                                    }


                                }
                                return RedirectToAction("List", "Info", new { @area = "Admin" });

                            }
                            else
                            {
                                ModelState.AddModelError("", "Priority value already exists");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", model.InfoName + " already exists");
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
        public ActionResult GetGroupList()
        {

            try
            {

                IList<Groups> GroupList = _groupService.GetAllGroups().Where(t => t.GroupName != null).ToList();
                List<Groups> GroupsLi = new List<Groups>();
                if (GroupList.Count > 0)
                {
                    foreach (var item in GroupList)
                    {
                        Groups groups = new Groups();
                        groups.GroupID = item.GroupID;
                        groups.GroupName = item.GroupName;
                        GroupsLi.Add(groups);

                    }


                }

                else
                {

                    ModelState.AddModelError("", "Error");

                }

                return Json(GroupsLi, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();


            }
        }

        [HttpGet]
        public ActionResult EditInfo(string id)
        
        {
            InfoModel model = new InfoModel();
            long ID = Convert.ToInt64(id);
            string groupname = null;
            string groupids = null;

            var varEditInfo = _infosService.GetAllInfosByQueryable().Where(t => t.InfoID == ID).FirstOrDefault();
            if (varEditInfo != null)
            {
                model.InfoID = varEditInfo.InfoID;
                model.InfoName = varEditInfo.InfoName;
                model.Title = varEditInfo.Title;
                model.Priority = varEditInfo.Priority;

                if (varEditInfo.Status == true)
                    model.Status = "1";
                else
                    model.Status = "0";


            }
            if (model.InfoID > 0)
            {

                List<InfoGroups> infogroupList = _infoGroupsService.GetAllInfoGroupsByQueryable().Where(t => t.InfoID == ID).ToList();

                model.InfoGroupList = infogroupList;


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
        public ActionResult EditInfo(InfoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model != null)
                    {
                        Info info = new Info();
                        var varEditInfo = _infosService.GetAllInfosByQueryable().Where(t => t.InfoID == model.InfoID);
                        if (varEditInfo != null)
                        {
                            if (varEditInfo.Where(t => t.InfoID == model.InfoID).Count() != 0)
                            {
                                info.InfoID = model.InfoID;
                                info.InfoName = model.InfoName;
                                info.Title = model.Title;
                                info.Priority = model.Priority;
                                info.Status = Convert.ToBoolean(model.Status == "1" ? true : false);
                                _infosService.updateInfo(info);
                                if (model.InfoID > 0)
                                {
                                    List<InfoGroups> infogroupList = _infoGroupsService.GetAllInfoGroupsByQueryable().Where(t => t.InfoID == model.InfoID).ToList();
                                    model.InfoGroupList = infogroupList;
                                    string groups = model.GroupIDs;
                                    groups = groups.Trim(',');
                                    string[] strgroups = groups.Split(',');
                                    _infoGroupsService.deleteInfoGroups(model.InfoID);
                                    foreach (var item in strgroups)
                                    {
                                        InfoGroups infogroup = new InfoGroups();
                                        infogroup.InfoID = info.InfoID;
                                        infogroup.GroupID = Convert.ToInt32(item);
                                        _infoGroupsService.insertNewInfoGroups(infogroup);
                                    }

                                }

                                return RedirectToAction("List", "Info", new { @area = "Admin" });
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
        public ActionResult DeleteInfo(long id)
        {
            try
            {
                List<InfoGroups> infogroupExists = _infoGroupsService.GetAllInfoGroupsByQueryable().Where(t => t.InfoID == id).ToList();
                Info infoExists = _infosService.GetAllInfosByQueryable().Where(t => t.InfoID == id).FirstOrDefault();
                if (infoExists != null)
                {
                    if (infogroupExists.Count > 0)
                    {
                        //foreach (var item in infogroupExists)
                        //{
                        _infoGroupsService.deleteInfoGroups(id);
                        //}

                        _infosService.deleteInfo(infoExists);
                        return Json("true", JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        _infosService.deleteInfo(infoExists);
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

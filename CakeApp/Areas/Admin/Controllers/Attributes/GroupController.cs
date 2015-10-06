using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CakeApp.Areas.Admin.Models.Attributes;
using Service.Group;
using Core.Group;

namespace CakeApp.Areas.Admin.Controllers.Attributes
{
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
 
        }
        public ActionResult List()
        {
            List<GroupModel> model = new List<GroupModel>();
            try
            {
                IList<Groups> groupList = _groupService.GetAllGroups().ToList();

                if (groupList.Count > 0)
                {
                    foreach (var group in groupList)
                    {

                        model.Add(new GroupModel
                        {
                            GroupID = group.GroupID,
                            GroupName = group.GroupName,
                            Title = group.Title,

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
        public ActionResult AddGroup()
        {
            GroupModel model = new GroupModel();

            return View(model);
          
        }
        [HttpPost]
        public ActionResult AddGroup(GroupModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var groupExists = _groupService.GetAllGroupsByQueryable();
                    if (groupExists != null)
                    {
                        if (groupExists.Where(t => t.GroupName == model.GroupName).Count() == 0)
                        {
                            
                            Groups group = new Groups();
                            group.GroupName = model.GroupName;
                            group.Title = model.Title;

                            _groupService.insertNewGroup(group);
                            model.GroupID = group.GroupID;

                            return RedirectToAction("List", "Group", new { @area = "Admin" });

                        }
                        else
                        {
                            ModelState.AddModelError("", model.GroupName + " already exists");
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
        public ActionResult DeleteGroup(long id)
        {
            try
            {
                Groups groupExists = _groupService.GetAllGroupsByQueryable().Where(t => t.GroupID == id).FirstOrDefault();
                if (groupExists != null)
                {
                    _groupService.deleteGroup(groupExists);
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
        public ActionResult EditGroups(GroupModel updateGroupModel)
        {
            try
            {

                if (updateGroupModel.GroupID > 0)
                {
                    Groups group = new Groups();
                    group.GroupID = updateGroupModel.GroupID;
                    group.GroupName = updateGroupModel.GroupName;
                    group.Title = updateGroupModel.Title;

                    _groupService.updateGroups(group);

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

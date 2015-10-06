using CakeApp.Areas.Admin.Models.FTPSetting;
using Core.FTPSetting;
using Service.FTPSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeApp.Areas.Admin.Controllers.FTPSetting
{
    public class FTPSettingsController : Controller
    {
        private readonly IFTPSettingService _ftpSettingService;

        public FTPSettingsController(IFTPSettingService ftpSettingService)
        {
            _ftpSettingService = ftpSettingService;
        }
        //
        // GET: /Admin/FTPSettings/
        public ActionResult Index()
        {
            FTPSettingsModel model = new FTPSettingsModel();
            FTPSettings details = _ftpSettingService.GetAllFTPSettingByQueryable().Where(f => f.FTPSettingsID == 1).FirstOrDefault();
            if (details != null)
            {
                model.HostName = details.HostName;
                model.FTPPassword = details.FTPPassword;
                model.FolderName = details.FolderName;
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(FTPSettingsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    FTPSettings objFTPSettings = new FTPSettings();
                    objFTPSettings.HostName = model.HostName;
                    objFTPSettings.FTPPassword = model.FTPPassword;
                    objFTPSettings.FolderName = model.FolderName;
                    _ftpSettingService.InsertFTPSetting(objFTPSettings);
                }
                
            }
            catch (Exception ex)
            {
               
                    ModelState.AddModelError("", ex.InnerException.Message);
              
            }
            

            //FTPSettings details = _ftpSettingService.GetAllFTPSettingByQueryable().Where(f => f.FTPSettingsID == 1).FirstOrDefault();
            //if (details != null)
            //{
            //    model.HostName = details.HostName;
            //    model.FTPPassword = details.FTPPassword;
            //    model.FolderName = details.FolderName;
            //}

            return View(model);
        }
	}
}
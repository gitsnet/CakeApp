using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace CakeApp.Areas.Admin.Controllers.Sync
{
    public class SyncProcessController : BaseController
    {
        public ActionResult SyncProcess()
        {
            return View();
        }
        public ActionResult AddSync()
        {
            return View();
        }
        public ActionResult SyncEmail()
        {
            return View();
        }
        public ActionResult SyncFtp()
        {
            return View();
        }
    }
}

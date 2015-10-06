using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeApp.Areas.Admin.Controllers.DashBoard
{
    public class DashBoardController : BaseController
    {
        //
        // GET: /Admin/DashBoard/
        public ActionResult Index()
        {
            return View();
        }
	}
}
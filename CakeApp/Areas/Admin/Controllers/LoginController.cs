using CakeApp.Areas.Admin.Models;
using Service.Authentication;
using Service.Common;
using Service.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CakeApp.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        AuthenticationService oAuth = new AuthenticationService();
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }
        public ActionResult Index(string returnUrl)
        {
            LoginViewModel model = new LoginViewModel();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDetails = _userService.IsValidUser(model.UserName,CommonMethods.CreateMD5(model.Password));
                  
                    if (userDetails != null)
                    {
                        oAuth.SignIn(userDetails, true);
                        return RedirectToLocal(returnUrl);
                       
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Username or Password.");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Required Username & Password");
                }

               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);                
            }

            return View(model);
        }
        public ActionResult Logout()
        {
            oAuth.SignOut();
            return RedirectToAction("Index", "Login", new { area = "Admin" });

        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Dashboard", new {@area="Admin" });
            }
        }
    }
}
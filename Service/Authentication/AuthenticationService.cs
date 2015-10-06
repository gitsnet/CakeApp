using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Core.User;


namespace Service.Authentication
{
    public class AuthenticationService 
    {
        
        public void SignIn(Users User, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                User.Username,
                now,
                now.AddMinutes(60),
                createPersistentCookie,
                User.Username,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            HttpContext _httpContext = HttpContext.Current;
            _httpContext.Response.Cookies.Add(cookie);          
        }
        public void SignOut()
        {

            FormsAuthentication.SignOut();
        }
    }
}

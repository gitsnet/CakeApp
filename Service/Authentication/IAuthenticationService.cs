using Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Authentication
{
    public partial interface IAuthenticationService
    {
        void SignIn(Users User, bool createPersistentCookie);
        void SignOut();
    }
}

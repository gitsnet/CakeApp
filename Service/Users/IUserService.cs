using Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.User
{
    public partial interface IUserService
    {
        #region User

        void Deleteuser(Users user);
        Users GetUserById(long userId);
        IList<Users> GetAllUser();
        Users InsertUser(Users user);
        Users UpdateUser(Users user);
        IQueryable<Users> GetAllUserQueryable();
        Users IsValidUser(string username, string password);
        bool IsUserNameExists(string username);
        Users GetUserByUserName(string username);

        #endregion
    }
}

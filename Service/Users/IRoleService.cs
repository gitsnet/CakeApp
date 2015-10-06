using Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.User
{
    public partial interface IRoleService
    {
        #region Userrole

        void DeleteUserRole(Role userrole);
        void InsertUserRole(Role userrole);
        void UpdateUserRole(Role userrole);
        IList<Role> GetAllUserRole();
        IQueryable<Role> GetAllUserRoleQueryable();

        #endregion
    }
}

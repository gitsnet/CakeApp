using Core.Category;
using Core.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Group
{
    public partial interface IGroupService 
    {
        #region Group
        Groups insertNewGroup(Groups group);
        IQueryable<Groups> GetAllGroupsByQueryable();
        IList<Groups> GetAllGroups();
        Groups updateGroups(Groups group);
        Groups deleteGroup(Groups group);
        #endregion
    }
}

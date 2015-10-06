using Core.Category;
using Core.Data;
using Core.Group;
using Service.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Group
{
    public partial class GroupService : IGroupService
    {
        private readonly IRepository<Groups> _groupsService;
        public GroupService(IRepository<Groups> groupsService)
        {
            _groupsService = groupsService; 
        }
        public Groups insertNewGroup(Groups group)
        {
            if (group != null)
            {
                _groupsService.Insert(group);
                return group;
            } 
            else 
            {
                return null;
            }
        }
        public IQueryable<Groups> GetAllGroupsByQueryable()
        {
            return _groupsService.Table;
        }

        public IList<Groups> GetAllGroups()
        {
            return _groupsService.Table.ToList();
        }
        public Groups updateGroups(Groups group)
        {
            if (group != null)
            {
                Groups result = _groupsService.GetById(group.GroupID);
                result.GroupID = group.GroupID;
                result.GroupName = group.GroupName;
                result.Title = group.Title;

                _groupsService.Update(result);

                return result;
            }
            else
            {
                return null;
            }
        }
        public Groups deleteGroup(Groups group)
        {
            if (group != null)
            {
                Groups result = _groupsService.GetById(group.GroupID);
                if (result != null)
                {
                    _groupsService.Delete(result);
                    return result;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}

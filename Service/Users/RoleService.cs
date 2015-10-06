using Core.Data;
using Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.User
{
    public partial class RoleService : IRoleService
    {
        private readonly IRepository<Role> _userroleRepository;

        public RoleService(IRepository<Role> userroleRepository)
        {
            _userroleRepository = userroleRepository;
        }

        public void DeleteUserRole(Role userrole)
        {
            if(userrole == null)
                throw new ArgumentNullException("userrole");
            userrole.Status = false;
            _userroleRepository.Update(userrole);
        }

        public void InsertUserRole(Role userrole)
        {
            if(userrole==null)
                throw new ArgumentNullException("userrole");
            _userroleRepository.Insert(userrole);
        }

        public void UpdateUserRole(Role userrole)
        {
            if(userrole == null)
                throw new ArgumentNullException("userrole");
            _userroleRepository.Update(userrole);
        }

        public IList<Role> GetAllUserRole()
        {
            return _userroleRepository.Table.ToList<Role>();
        }

        public IQueryable<Role> GetAllUserRoleQueryable()
        {
            return _userroleRepository.Table;
        }
    }
}

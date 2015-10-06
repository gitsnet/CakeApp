using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.User
{
    public partial class Role : BaseEntity
    {
        //public Role()
        //{
        //    this.Users = new List<Users>();
        //}
        public virtual long RoleID { get; set; }
        public virtual string RoleName { get; set; }
        public virtual bool Status { get; set; }
        //public virtual ICollection<Users> Users { get; set; }
    }
} 

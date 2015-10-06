using Core.Category;
using Core.Product;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.User
{
    public partial class Users : BaseEntity
    {
        //public Users()
        //{
        //    this.Categories = new List<Categories>();
        //    this.Products = new List<Products>();
        //}
        public virtual long UserID { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual long? RoleID { get; set; }
        public virtual bool Status { get; set; }
        public virtual Role Role { get; set; }
        //public virtual ICollection<Categories> Categories { get; set; }
        //public virtual ICollection<Products> Products { get; set; }
    }
}
 
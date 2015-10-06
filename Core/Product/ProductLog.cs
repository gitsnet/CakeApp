using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Product
{
    public partial class ProductLog:BaseEntity
    {
        public virtual long ProductLogID { get; set; }
        public virtual Nullable<long> ProductID { get; set; }
        public virtual bool IsAdded { get; set; }
        public virtual bool IsUpdated { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual DateTime DateOfModification { get; set; }
        public virtual Products Products { get; set; }
       
    }
}

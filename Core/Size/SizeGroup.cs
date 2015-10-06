using Core.Group;
using Core.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Size
{
  public partial class SizeGroup:BaseEntity
    {
        public long SizeGroupID { get; set; }
        public Nullable<long> SizeID { get; set; }
        public Nullable<long> GroupID { get; set; }
        public virtual Groups Groups { get; set; }
        public virtual Sizes Sizes { get; set; }
    }
}

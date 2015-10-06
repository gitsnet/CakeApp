using Core.Group;
using Core.Infos;
using Core.Size;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Product
{
  public partial  class ProductPrice:BaseEntity
    {
        public virtual long ProductPriceID { get; set; }
        public virtual long? ProductID { get; set; }
        public virtual long? GroupID { get; set; }
        //public virtual long? InfoID { get; set; }
        public virtual long? SizeID { get; set; }
        public virtual string InfoIDs { get; set; }

        public virtual Products Product { get; set; }
        public virtual Groups Group { get; set; }
        //public virtual Info Infos { get; set; }
        public virtual Sizes Sizes { get; set; }
        public virtual decimal? Price { get; set; }
    }
}

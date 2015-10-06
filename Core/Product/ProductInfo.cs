using Core.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Product
{
    public partial class ProductInfo: BaseEntity
    {
        public long ProductInfoID { get; set; }
        public Nullable<long> ProductID { get; set; }
        public Nullable<long> InfoID { get; set; }
        public virtual Info Info { get; set; }
        public virtual Products Products { get; set; }
    }
}

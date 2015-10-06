using Core.Product;
using System;
using System.Collections.Generic;

namespace Core.Size
{
    public partial class Sizes : BaseEntity
    {
       
        public virtual long SizeID { get; set; }
        public virtual string Size { get; set; }
        public virtual string Title { get; set; }
        public virtual Nullable<int> Priority { get; set; }
        public virtual Nullable<bool> Status { get; set; }
        public virtual string Price { get; set; }
        public virtual ICollection<ProductSizes> ProductSizes { get; set; }
        public virtual ICollection<SizeGroup> SizeGroup { get; set; }
        public virtual ICollection<ProductPrice> ProductPrice { get; set; }
    }
}

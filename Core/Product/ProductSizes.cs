using Core.Size;
using System;
using System.Collections.Generic;

namespace Core.Product
{
    public partial class ProductSizes : BaseEntity
    {
        public virtual long ProductSizeID { get; set; }
        public virtual Nullable<long> ProductID { get; set; }
        public virtual Nullable<long> SizeID { get; set; }
        public virtual Products Products { get; set; }
        public virtual Sizes Sizes { get; set; }
    }
}

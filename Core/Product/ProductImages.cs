using System;
using System.Collections.Generic;

namespace Core.Product
{
    public partial class ProductImages : BaseEntity
    {
        public virtual long ProductImageID { get; set; }
        public virtual Nullable<long> ProductID { get; set; }
        public virtual string ImageName { get; set; }
        public virtual string Title { get; set; }
        public virtual string ImageAlt { get; set; }
        public virtual int Priority { get; set; } 
        public virtual Nullable<bool> Status { get; set; }
        public virtual Products Products { get; set; }
    }
}

using Core.Group;
using Core.Sync;
using Core.Tag;
using Core.User;
using System;
using System.Collections.Generic;

namespace Core.Product
{
    public partial class Products : BaseEntity
    {

        public virtual long ProductID { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string ProductImageName { get; set; }
        public virtual string Title { get; set; }
        public virtual Nullable<long> TagID { get; set; }
        public virtual int Priority { get; set; }
        public virtual Nullable<long> GroupID { get; set; }
        public virtual string AdditionalInfo { get; set; }
        public virtual string Instructions { get; set; }
        public virtual Nullable<long> AddedBy { get; set; }
        public virtual bool Status { get; set; }
        public virtual bool statusAdditionalInfo { get; set; }
        public virtual bool statusInstructions { get; set; }
        public virtual Groups Groups { get; set; }
        public virtual ICollection<ProductImages> ProductImages { get; set; }
        public virtual Tags Tags { get; set; } 
        public virtual ICollection<ProductSizes> ProductSizes { get; set; }
        //public virtual ICollection<SyncProcess> SyncProcess { get; set; }
        public virtual ICollection<ProductInfo> ProductInfo { get; set; }
        public virtual ICollection<ProductCategories> ProductCategories { get; set; }
        public virtual ICollection<ProductPrice> ProductPrice { get; set; }
        public virtual ICollection<ProductLog> ProductLog { get; set; }
    }
}

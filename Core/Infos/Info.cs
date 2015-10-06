using Core.Group;
using Core.Product;
using System;
using System.Collections.Generic;

namespace Core.Infos
{
    public partial class Info : BaseEntity
    {
        //public virtual long InfoID { get; set; }
        //public virtual string InfoName { get; set; }
        //public virtual string Title { get; set; }
        
        //public virtual Nullable<int> Priority { get; set; }
        //public virtual Nullable<bool> Status { get; set; }
        public Info()
        {
            this.InfoGroups = new List<InfoGroups>();
            this.ProductInfo = new List<ProductInfo>();
        }

        public long InfoID { get; set; }
        public string InfoName { get; set; }
        public string Title { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<bool> Status { get; set; }
        public virtual ICollection<InfoGroups> InfoGroups { get; set; }
        public virtual ICollection<ProductInfo> ProductInfo { get; set; }
        //public virtual ICollection<ProductPrice> ProductPrice { get; set; }
      
    }
}

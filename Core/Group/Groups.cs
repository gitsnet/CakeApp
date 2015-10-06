using Core.Infos;
using Core.Product;
using Core.Size;
using System;
using System.Collections.Generic;

namespace Core.Group
{
    public partial class Groups : BaseEntity
    {
        //public Groups()
        //{
        ////    this.Info = new List<Info>();
        ////    this.Products = new List<Products>();
        //}

        //public virtual long GroupID { get; set; }
        //public virtual string GroupName { get; set; }
        //public virtual string Title { get; set; }
        //public virtual ICollection<Info> Info { get; set; }
        //public virtual ICollection<Products> Products { get; set; }
        public Groups()
        {
            this.InfoGroups = new List<InfoGroups>();
            this.Products = new List<Products>();
        }

        public long GroupID { get; set; }
        public string GroupName { get; set; }
        public string Title { get; set; }
        public virtual ICollection<InfoGroups> InfoGroups { get; set; }
        public virtual ICollection<Products> Products { get; set; }
        public virtual ICollection<SizeGroup> SizeGroup { get; set; }
        public virtual ICollection<ProductPrice> ProductPrice { get; set; }
    }
}

using Core.User;
using System;
using System.Collections.Generic;
using Core.Tag;
using Core.Product;
namespace Core.Category
{
    public partial class Categories : BaseEntity
    {
        //public virtual long CategoryID { get; set; }
        //public virtual string CategoryName { get; set; }
        //public virtual string Title { get; set; }
        //public virtual Nullable<long> TagID { get; set; }
        //public virtual Nullable<int> Priority { get; set; }
        //public virtual bool Status { get; set; }
        //public virtual Nullable<long> AddedBy { get; set; }
        //public virtual Tags Tags { get; set; }
        //public virtual Users Users { get; set; }
        //public virtual IList<CategoryImages> CategoryImages { get; set; }
       

        public long CategoryID { get; set; }
        public Nullable<long> CategoryImageID { get; set; }
        public string CategoryName { get; set; }

        public string CategoryImageName{ get; set; }
        public string Title { get; set; }
        public Nullable<long> TagID { get; set; }
        public Nullable<int> Priority { get; set; }
        public bool Status { get; set; }
        public Nullable<long> AddedBy { get; set; }
        public virtual CategoryImages CategoryImages { get; set; }
        public virtual Tags Tags { get; set; }
        public virtual ICollection<ProductCategories> ProductCategories { get; set; }

    }
}

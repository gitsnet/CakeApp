using Core.Category;
using Core.Product;
using System;
using System.Collections.Generic;

namespace Core.Tag
{
    public partial class Tags : BaseEntity
    {
        //public virtual long TagID { get; set; }
        //public virtual string TagName { get; set; }
        //public virtual string Title { get; set; }
        //public virtual ICollection<Categories> Categories { get; set; }
        //public virtual ICollection<Products> Products { get; set; }
        public Tags()
        {
            this.Categories = new List<Categories>();
            this.Products = new List<Products>();
        }

        public long TagID { get; set; }
        public string TagName { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Categories> Categories { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}

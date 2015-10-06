using Core.User;
using System;
using System.Collections.Generic;
using Core.Tag;
namespace Core.Category
{
    public partial class CategoryImages : BaseEntity
    {
        public virtual long CategoryImageID { get; set; }        
        public virtual string Title { get; set; }
        public virtual string ImageName { get; set; }
        public virtual string ImageAlt { get; set; }
        public virtual int Priority { get; set; } 
        public virtual bool Status { get; set; }      
        public virtual List<Categories> Categories { get; set; }


    }
}

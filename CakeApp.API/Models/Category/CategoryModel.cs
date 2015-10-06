using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CakeApp.API.Models.Category
{
    public class CategoryModel
    {

        public CategoryModel()
        {
            DefaultImage = new CategoryImageModel();
            Tag = new TagModel();

        }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public CategoryImageModel DefaultImage { get; set; }
        
        public TagModel Tag { get; set; }
       
        public string Priority { get; set; }
        //public bool Status { get; set; }

    }
    public class CategoryImageModel
    {
        public string ImageName { get; set; }
        //public string ImageAlt { get; set; }
    }
    public class TagModel
    {
        public string TagName { get; set; }
        public string TagID { get; set; }
    }
}
using Core.Tag;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Core.Category;

namespace CakeApp.Areas.Admin.Models.Category
{
    public class CategoriesModel
    {
        public CategoriesModel()
        {
            TagList = new List<Tags>();
            CategoryImagesList = new List<CategoryImages>();
        }
        public long CategoryID { get; set; }
        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Tag is required.")]
        public string TagID { get; set; }
        public string TagName { get; set; }
         [Required(ErrorMessage = "Image is required.")]
        public  long CategoryImageID { get; set; }
        public  string ImageName { get; set; }
        public string Image { get; set; }
         [Required]
         [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? Priority { get; set; }
        public string Status { get; set; }


        public List<Tags> TagList { get; set; }
        public List<CategoryImages> CategoryImagesList { get; set; }


    }
}
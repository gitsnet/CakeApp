using Core.Tag;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Core.Category;

namespace CakeApp.Areas.Admin.Models.Category
{
    public class CategoryImagesModel
    {
       
        public long CategoryImageID { get; set; }
        
        public string ImageName { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Alternative Text is required.")]
        public string ImageAlt { get; set; }
         [Required]
         [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? Priority { get; set; }
        public string Status { get; set; }

    }
}
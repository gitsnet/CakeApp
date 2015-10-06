using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CakeApp.Areas.Admin.Models.Attributes
{
    public class TagModel
    {
        public long TagID { get; set; }
        [Required(ErrorMessage = "Tag Name is required.")]
        public string TagName { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
    }
}
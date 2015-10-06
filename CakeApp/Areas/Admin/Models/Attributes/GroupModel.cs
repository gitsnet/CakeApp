using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CakeApp.Areas.Admin.Models.Attributes
{
    public class GroupModel
    {
        public long GroupID { get; set; }
        [Required(ErrorMessage = "Group Name is required.")]
        public string GroupName { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CakeApp.Areas.Admin.Models.Screensaver
    
{
    public class ScreensaversModel
    {
         public long ScreensaverID { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        public string Video { get; set; }
        public string Status { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? Priority { get; set; }
    }
}
using Core.Group;
using Core.Size;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CakeApp.Areas.Admin.Models.Attributes
{
    public class SizeModel
    {
        public SizeModel()
        {
            GroupList = new List<Groups>();
            SizeGroupList = new List<SizeGroup>();
          
 
        }
        public long SizeID { get; set; }
        [Required(ErrorMessage = "Size Name is required.")]
        public string Size { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        //[Required(ErrorMessage = "Group is required.")]
         public string GroupID { get; set; }
        public string GroupName { get; set; }

        [Required(ErrorMessage = "At least one Group must be selected")]
        public string GroupIDs { get; set; }
         [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? Priority { get; set; }
        public string Status { get; set; }
       public  string GroupNames { get; set; }

       //  [Required(ErrorMessage = "Price is required.")]
       //[RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Price can't have more than 2 decimal places")]
       //public decimal Price { get; set; }

        public List<Groups> GroupList { get; set; }
        public List<SizeGroup> SizeGroupList { get; set; }
    }
}
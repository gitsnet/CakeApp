using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Core.Group;
using Core.Infos;

namespace CakeApp.Areas.Admin.Models.Attributes
{
    public class InfoModel
    {
        public InfoModel()
        {
            GroupList = new List<Groups>();
            InfoGroupList = new List<InfoGroups>();
 
        }
        public long InfoID { get; set; }
        [Required(ErrorMessage = "Info Name is required.")]
        public string InfoName { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Group is required.")]
        public string GroupIDs { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
         [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int? Priority { get; set; }
        public string Status { get; set; }
       public  string GroupNames { get; set; }
       public List<InfoGroups> InfoGroupList { get; set; }

        public List<Groups> GroupList { get; set; }
    }
}
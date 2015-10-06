using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CakeApp.Areas.Admin.Models.FTPSetting
{
    public class FTPSettingsModel
    {
        public long FTPSettingsID { get; set; }
        [Required(ErrorMessage="HostName Required")]
        public String HostName { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public String FTPPassword { get; set; }
        [Required(ErrorMessage = "Folder Name Required")]
        public String FolderName { get; set; }
    }
}
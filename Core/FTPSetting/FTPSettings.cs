using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FTPSetting
{
    public partial class FTPSettings : BaseEntity
    {
        public long FTPSettingsID { get; set; }
        public String HostName { get; set; }
        public String FTPPassword { get; set; }
        public String FolderName { get; set; }
    }
}

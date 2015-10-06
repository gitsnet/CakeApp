using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Screensaver
{
   public partial class Sreensavers:BaseEntity
    {
        public virtual long ScreensaverID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Title { get; set; }
        public virtual string Video { get; set; }
        public virtual Nullable<int> Priority { get; set; }
        public virtual bool Status { get; set; }
        
     
    }
}

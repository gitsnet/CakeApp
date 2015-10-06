using Core.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infos
{
    public partial class InfoGroups: BaseEntity
    {
        public long InfoGroupID { get; set; }
        public Nullable<long> InfoID { get; set; }
        public Nullable<long> GroupID { get; set; }
        public virtual Groups Groups { get; set; }
        public virtual Info Info { get; set; }
    }
}

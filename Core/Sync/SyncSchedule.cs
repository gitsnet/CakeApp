using System;
using System.Collections.Generic;

namespace Core.Sync
{
    public partial class SyncSchedule : BaseEntity
    {
        public SyncSchedule()
        {
            this.SyncProcess = new List<SyncProcess>();
        }

        public virtual long SyncID { get; set; }
        public virtual string SyncName { get; set; }
        public virtual Nullable<System.DateTime> Date { get; set; }
        public virtual string Time { get; set; }
        public virtual Nullable<bool> Status { get; set; }
        public virtual ICollection<SyncProcess> SyncProcess { get; set; }
    }
}

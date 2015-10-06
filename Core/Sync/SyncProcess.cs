using Core.Product;
using System;
using System.Collections.Generic;

namespace Core.Sync
{
    public partial class SyncProcess : BaseEntity
    {
        public virtual long SyncProcessID { get; set; }
        public virtual Nullable<long> SyncID { get; set; }
        public virtual Nullable<long> ProductID { get; set; }
        public virtual Nullable<int> SyncStatus { get; set; }
        public virtual Products Products { get; set; }
        public virtual SyncSchedule SyncSchedule { get; set; }
    }
}

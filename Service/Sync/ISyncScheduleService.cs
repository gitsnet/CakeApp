using Core.Group;
using Core.Size;
using Core.Sync;
using System.Collections.Generic;
using System.Linq;

namespace Service.Sync
{
    public partial interface ISyncScheduleService
    {
        #region SyncSchedule
        SyncSchedule insertNewSyncSchedule(SyncSchedule schedule);
        IQueryable<SyncSchedule> GetAllSyncScheduleByQueryable();
        IList<SyncSchedule> GetAllSyncSchedule();
        SyncSchedule updateSyncSchedule(SyncSchedule schedule); 
        #endregion
    }
} 

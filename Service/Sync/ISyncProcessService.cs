using Core.Group;
using Core.Size;
using Core.Sync;
using System.Collections.Generic;
using System.Linq;

namespace Service.Sync
{
    public partial interface ISyncProcessService 
    {
        #region Size
        SyncProcess insertNewSyncProcess(SyncProcess process);
        IQueryable<SyncProcess> GetAllSyncProcesssByQueryable();
        IList<SyncProcess> GetAllSyncProcess();
        SyncProcess updateSyncProcess(SyncProcess process); 
        #endregion
    }
}

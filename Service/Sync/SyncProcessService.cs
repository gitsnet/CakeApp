using Core.Data;
using Core.Sync;
using System.Collections.Generic;
using System.Linq;

namespace Service.Sync 
{
    public partial class SyncProcessService : ISyncProcessService  
    {
        private readonly IRepository<SyncProcess> _processService;
        public SyncProcessService(IRepository<SyncProcess> processService)
        {
            _processService = processService; 
        }
        public SyncProcess insertNewSyncProcess(SyncProcess process)
        {
            if (process != null)
            {
                _processService.Insert(process);
                return process;
            } 
            else 
            {
                return null;
            }
        }
        public IQueryable<SyncProcess> GetAllSyncProcesssByQueryable()
        {
            return _processService.Table;
        }

        public IList<SyncProcess> GetAllSyncProcess()
        {
            return _processService.Table.ToList();
        }
        public SyncProcess updateSyncProcess(SyncProcess process)
        {
            if (process != null)
            {
                _processService.Update(process);
                return process;
            }
            else
            {
                return null;
            }
        }
    }
}

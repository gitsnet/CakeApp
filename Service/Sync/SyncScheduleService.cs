using Core.Data;
using Core.Sync;
using System.Collections.Generic;
using System.Linq;

namespace Service.Sync
{
    public partial class SyncScheduleService : ISyncScheduleService  
    {
        private readonly IRepository<SyncSchedule> _scheduleService;
        public SyncScheduleService(IRepository<SyncSchedule> scheduleService)
        {
            _scheduleService = scheduleService; 
        }
        public SyncSchedule insertNewSyncSchedule(SyncSchedule schedule)
        {
            if (schedule != null)
            {
                _scheduleService.Insert(schedule);
                return schedule;
            } 
            else 
            {
                return null;
            }
        }
        public IQueryable<SyncSchedule> GetAllSyncScheduleByQueryable()
        {
            return _scheduleService.Table;
        }

        public IList<SyncSchedule> GetAllSyncSchedule()
        {
            return _scheduleService.Table.ToList();
        }
        public SyncSchedule updateSyncSchedule(SyncSchedule schedule)
        {
            if (schedule != null)
            {
                _scheduleService.Update(schedule);
                return schedule;
            }
            else
            {
                return null;
            }
        }
    }
}

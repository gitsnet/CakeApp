using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Screensaver;
using Core.Data;
using Service.Screensaver;

namespace Service.Screensaver
{
  public partial  class ScreensaverService:IScreensaverService
    {
      private readonly IRepository<Sreensavers> _screensaversService;
      public ScreensaverService(IRepository<Sreensavers> screensaversService)
      {
          _screensaversService = screensaversService;
 
      }
      public Sreensavers insertNewSreensaver(Sreensavers sreensavers)
        {
            if (sreensavers != null)
            {
                _screensaversService.Insert(sreensavers);
                return sreensavers;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<Sreensavers> GetAllSreensaversQueryable()
        {
            return _screensaversService.Table;
        }

        public IList<Sreensavers> GetAllSreensavers()
        {
           return _screensaversService.Table.ToList();
        }

        public Sreensavers updateSreensaver(Sreensavers sreensavers)
        {
            if (sreensavers != null)
            {
                Sreensavers result = _screensaversService.GetById(sreensavers.ScreensaverID);
                result.ScreensaverID = sreensavers.ScreensaverID;
                result.Name = sreensavers.Name;
                result.Title = sreensavers.Title;
                result.Priority = sreensavers.Priority;
                result.Status = sreensavers.Status;
                result.Video = sreensavers.Video;

                _screensaversService.Update(result);
                
                return result;
            }
            else
            {
                return null;
            }
        }

        public Sreensavers deleteSreensaver(Sreensavers sreensavers)
        {
            if (sreensavers != null)
            {
                Sreensavers result = _screensaversService.GetById(sreensavers.ScreensaverID);

                _screensaversService.Delete(result);
                return sreensavers;
            }
            else
            {
                return null;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Screensaver;

namespace Service.Screensaver
{
    public partial interface IScreensaverService
    {
        Sreensavers insertNewSreensaver(Sreensavers sreensavers);
        IQueryable<Sreensavers> GetAllSreensaversQueryable();
        IList<Sreensavers> GetAllSreensavers();
        Sreensavers updateSreensaver(Sreensavers sreensavers);
        Sreensavers deleteSreensaver(Sreensavers sreensavers);
    }
}

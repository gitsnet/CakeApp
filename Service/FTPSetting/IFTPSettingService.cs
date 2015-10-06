using Core.FTPSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FTPSetting
{
    public partial interface IFTPSettingService
    {
        FTPSettings InsertFTPSetting(FTPSettings setting);
        FTPSettings UpdateFTPSetting(FTPSettings setting);
        FTPSettings DeleteFTPSetting(FTPSettings setting);
        IQueryable<FTPSettings> GetAllFTPSettingByQueryable();
        IList<FTPSettings> GetAllFTPSetting();
    }
}

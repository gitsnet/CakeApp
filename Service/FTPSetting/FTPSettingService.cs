using Core.FTPSetting;
using Core.Data;
using Service.FTPSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.FTPSetting
{
    public partial class FTPSettingService : IFTPSettingService
    {
        private readonly IRepository<FTPSettings> _FtpSettingService;
        public FTPSettingService(IRepository<FTPSettings> FtpSettingService)
        {
            _FtpSettingService = FtpSettingService;
        }
        public FTPSettings InsertFTPSetting(FTPSettings settings)
        {
            if (settings != null)
            {
                _FtpSettingService.Insert(settings);
                return settings;
            }
            else
            {
                return null;
            }
        }
        public IQueryable<FTPSettings> GetAllFTPSettingByQueryable()
        {
            return _FtpSettingService.Table;
        }

        public IList<FTPSettings> GetAllFTPSetting()
        {
            return _FtpSettingService.Table.ToList();
        }
        public FTPSettings UpdateFTPSetting(FTPSettings settings)
        {
            if (settings != null)
            {
                FTPSettings result = _FtpSettingService.GetById(settings.FTPSettingsID);
                result.FTPSettingsID = settings.FTPSettingsID;
                result.HostName = settings.HostName;
                result.FTPPassword = settings.FTPPassword;
                result.FolderName = settings.FolderName;

                  _FtpSettingService.Update(result);

                return result;
            }
            else
            {
                return null;
            }
        }

        public FTPSettings DeleteFTPSetting(FTPSettings settings)
        {
            if (settings != null)
            {
                FTPSettings result = _FtpSettingService.GetById(settings.FTPSettingsID);
            
            _FtpSettingService.Delete(result);
            return settings;
            }
            else
            {
                return null;
            }
 
        }
    }
}

using Core.Category;
using Core.Data;
using Core.Group;
using Core.Infos;
using Service.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.infos
{
    public partial class InfosService : IInfosService
    {
        private readonly IRepository<Info> _infosService;
        public InfosService(IRepository<Info> infosService)
        {
            _infosService = infosService;
        }
        public Info insertNewInfo(Info info)
        {
            if (info != null)
            {
                _infosService.Insert(info);
                return info;
            }
            else
            {
                return null;
            }
        }
        public IQueryable<Info> GetAllInfosByQueryable()
        {
            return _infosService.Table;
        }

        public IList<Info> GetAllInfos()
        {
            return _infosService.Table.ToList();
        }
        public Info updateInfo(Info info)
        {

            if (info != null)
            {
                Info result = _infosService.GetById(info.InfoID);

                result.InfoID = info.InfoID;
                result.InfoName = info.InfoName;
                result.Title = info.Title;
                //result.GroupID = info.GroupID;
                result.Priority = info.Priority;
                result.Status = info.Status;

                _infosService.Update(result);

                return result;
            }
            else
            {
                return null;
            }
        }
        public Info deleteInfo(Info info)
        {
            if (info != null)
            {
                Info result = _infosService.GetById(info.InfoID);

                _infosService.Delete(result);
                return info;
            }
            else
            {
                return null;
            }
 
        }

    }
}


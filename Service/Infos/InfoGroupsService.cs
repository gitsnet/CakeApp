using Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Infos;

namespace Service.Infos
{
    public partial class InfoGroupsService : IInfoGroupsService
    {
        private readonly IRepository<InfoGroups> _infoGroupService;
        public InfoGroupsService(IRepository<InfoGroups> infoGroupService)
        {
            _infoGroupService = infoGroupService;
        }
        public InfoGroups insertNewInfoGroups(InfoGroups InfoGroups)
        {
            if (InfoGroups != null)
            {
                _infoGroupService.Insert(InfoGroups);
                return InfoGroups;
            }
            else
            {
                return null;
            }
        }
        public IQueryable<InfoGroups> GetAllInfoGroupsByQueryable()
        {
            return _infoGroupService.Table;
        }
        public IList<InfoGroups> GetAllInfoGroups()
        {
            return _infoGroupService.Table.ToList();
        }
        public void deleteInfoGroups(long InfoID)
        {
            if (InfoID != null)
            {
                List<InfoGroups> result = _infoGroupService.Table.Where(a => a.InfoID == InfoID).ToList();
                foreach (InfoGroups item in result)
                {
                    _infoGroupService.Delete(item);
                }
                
            }
           
 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Infos;

namespace Service.Infos
{
    public partial interface IInfoGroupsService
    {
        InfoGroups insertNewInfoGroups(InfoGroups InfoGroups);
        IQueryable<InfoGroups> GetAllInfoGroupsByQueryable();
        IList<InfoGroups> GetAllInfoGroups();
        void deleteInfoGroups(long InfoID);
    }
}

using Core.Category;
using Core.Group;
using Core.Infos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.infos 
{
    public partial interface IInfosService 
    {
        #region Group
        Info insertNewInfo(Info info);
        IQueryable<Info> GetAllInfosByQueryable();
        IList<Info> GetAllInfos();
        Info updateInfo(Info info);
        Info deleteInfo(Info info);
        #endregion 
    }
}

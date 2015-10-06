using Core.Group;
using Core.Size;
using System.Collections.Generic;
using System.Linq;

namespace Service.Size
{ 
    public partial interface ISizeService 
    {
        #region Size
        Sizes insertNewSize(Sizes size); 
        IQueryable<Sizes> GetAllSizesByQueryable();
        IList<Sizes> GetAllSizes();
        Sizes updateSize(Sizes size);
        Sizes deleteSize(Sizes size);

        #endregion
    }
}

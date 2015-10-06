using Core.Size;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Size
{
    public partial interface ISizeGroupService
    {
        SizeGroup insertNewSizeGroup(SizeGroup sizeGroup);
        IQueryable<SizeGroup> GetAllSizeGroupsByQueryable();
        IList<SizeGroup> GetAllSizeGroups();
        void deleteSizeGroup(long SizeID);
    }
}

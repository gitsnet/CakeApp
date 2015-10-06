using Core.Data;
using Core.Size;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Size
{
  public partial  class SizeGroupService:ISizeGroupService
    {
      private readonly IRepository<SizeGroup> _sizeGroupService;
      public SizeGroupService(IRepository<SizeGroup> sizeGroupService)
        {
            _sizeGroupService = sizeGroupService;
        }
        public SizeGroup insertNewSizeGroup(SizeGroup sizeGroup)
        {
            if (sizeGroup != null)
            {
                _sizeGroupService.Insert(sizeGroup);
                return sizeGroup;
            }
            else
            {
                return null;
            }
        }

        public IQueryable<SizeGroup> GetAllSizeGroupsByQueryable()
        {
            return _sizeGroupService.Table;
        }

        public IList<SizeGroup> GetAllSizeGroups()
        {
            return _sizeGroupService.Table.ToList();
        }

        public void deleteSizeGroup(long SizeID)
        {
            if (SizeID != null)
            {
                List<SizeGroup> result = _sizeGroupService.Table.Where(a => a.SizeID == SizeID).ToList();
                
                foreach (SizeGroup item in result)
                {
                    _sizeGroupService.Delete(item);
                }

            }
        }
    }
}

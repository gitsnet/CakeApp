using Core.Data;
using Core.Size;
using System.Collections.Generic;
using System.Linq;

namespace Service.Size 
{
    public partial class SizeService : ISizeService 
    {
        private readonly IRepository<Sizes> _sizesService;
        public SizeService(IRepository<Sizes> sizesService)
        {
            _sizesService = sizesService; 
        }
        public Sizes insertNewSize(Sizes size)
        {
            if (size != null)
            {
                _sizesService.Insert(size);
                return size;
            } 
            else 
            {
                return null;
            }
        }
        public IQueryable<Sizes> GetAllSizesByQueryable()
        {
            return _sizesService.Table;
        }

        public IList<Sizes> GetAllSizes()
        {
            return _sizesService.Table.ToList();
        }
        public Sizes updateSize(Sizes size)
        {
            if (size != null)
            {
                Sizes result = _sizesService.GetById(size.SizeID);

                result.SizeID = size.SizeID;
                result.Size = size.Size;
                result.Title = size.Title;
                //result.GroupID = info.GroupID;
                result.Priority = size.Priority;
                result.Status = size.Status;
                result.Price = size.Price;

                _sizesService.Update(result);

                return result;
            }
            else
            {
                return null;
            }
        }
        public Sizes deleteSize(Sizes size)
        {
            if (size != null)
            {
                Sizes result = _sizesService.GetById(size.SizeID);

                _sizesService.Delete(result);
                return size;
            }
            else
            {
                return null;
            }
        }
    }
}

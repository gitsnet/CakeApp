using Core.Data;
using Core.Size;
using Core.Tag;
using Service.Tag;
using System.Collections.Generic;
using System.Linq;

namespace Service.Tag
{
    public partial class TagService : ITagService 
    {
        private readonly IRepository<Tags> _tagsService;
        public TagService(IRepository<Tags> tagsService)
        {
            _tagsService = tagsService; 
        }
        public Tags insertNewTag(Tags tag)
        {
            if (tag != null)
            {
                _tagsService.Insert(tag);
                return tag;
            } 
            else 
            {
                return null;
            }
        }
        public IQueryable<Tags> GetAllTagsByQueryable() 
        {
            return _tagsService.Table;
        }

        public IList<Tags> GetAllTags()
        {
            return _tagsService.Table.ToList();
        }
        public Tags updateTag(Tags tag)
        {
            if (tag != null)
            {
                Tags result = _tagsService.GetById(tag.TagID);
                result.TagID = tag.TagID;
                result.TagName = tag.TagName;
                result.Title = tag.Title;
                _tagsService.Update(result);

                return result;
            }
            else
            {
                return null;
            }
        }
        public Tags deleteTag(Tags tag)
        {
            if (tag != null)
            {
                Tags result = _tagsService.GetById(tag.TagID);
                if (result != null)
                {
                    _tagsService.Delete(result);
                    return result;
                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}

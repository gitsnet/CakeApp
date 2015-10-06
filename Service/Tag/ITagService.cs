using Core.Group;
using Core.Size;
using Core.Tag;
using System.Collections.Generic;
using System.Linq;

namespace Service.Tag
{
    public partial interface ITagService 
    {
        #region tag
        Tags insertNewTag(Tags tag);
        IQueryable<Tags> GetAllTagsByQueryable();
        IList<Tags> GetAllTags();
        Tags updateTag(Tags tag);
        Tags deleteTag(Tags tag);
        #endregion 
    }
}

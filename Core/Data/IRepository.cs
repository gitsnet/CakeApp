using System.Linq;

namespace Core.Data
{
    /// <summary>
    /// Repository
    /// </summary>
    public partial interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        ReturnMsg Insert(T entity);
        ReturnMsg Update(T entity);
        ReturnMsg Delete(T entity);
        IQueryable<T> Table { get; }
    }
}

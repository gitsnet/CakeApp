using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Core;
using Core.Data;

namespace Data
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDbContext context)
        {
            this._context = context;
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public ReturnMsg Insert(T entity)
        {
            ReturnMsg objReturnMsg = new ReturnMsg();
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                this._context.SaveChanges();
                objReturnMsg.IsSuccess = true;
                objReturnMsg.Message = "Inserted";

            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                objReturnMsg.IsSuccess = false;
                objReturnMsg.Message = msg;
                //Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
            return objReturnMsg;
        }

        public ReturnMsg Update(T entity)
        {
            ReturnMsg objReturnMsg = new ReturnMsg();
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this._context.SaveChanges();
                objReturnMsg.IsSuccess = true;
                objReturnMsg.Message = "Updated";
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                objReturnMsg.IsSuccess = false;
                objReturnMsg.Message = msg;
                throw fail;
            }
            return objReturnMsg;
        }

        public ReturnMsg Delete(T entity)
        {
            ReturnMsg objReturnMsg = new ReturnMsg();
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);

                this._context.SaveChanges();
                objReturnMsg.IsSuccess = true;
                objReturnMsg.Message = "Deleted";
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                //Debug.WriteLine(fail.Message, fail);
                objReturnMsg.IsSuccess = false;
                objReturnMsg.Message = msg;
                throw fail;
            }
            return objReturnMsg;
        }

        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        private IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using RestApi.Data.Entities;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestApi.Data.Infrastructure
{
    /// <summary>
    /// Abstract class RepositoryBase
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private AppDBContext dataContext;
        private readonly DbSet<T> dbSet;

        /// <summary>
        /// DbFactory
        /// </summary>
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        /// <summary>
        /// Enterty DBContext
        /// </summary>
        protected AppDBContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbFactory"></param>
        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #region Implementation

        /// <summary>
        /// Implement Add entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
             dbSet.Add(entity);
        }


        /// <summary>
        /// Implement Add list entity
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Add(List<T> entities)
        {
            foreach (T item in entities)
            {
                dbSet.Add(item);
            }
        }

        /// <summary>
        /// Implement Update entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(int entityId,T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Implement update list entity
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Update(List<T> entities)
        {
            foreach(T item in entities)
            {
                dbSet.Attach(item);
                dataContext.Entry(item).State = EntityState.Modified;
            }
        }


        /// <summary>
        /// Implement delete entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }


        /// <summary>
        /// Implement delete list entity
        /// </summary>
        /// <param name="entities"></param>
        public virtual void Delete(List<T> entities)
        {
            foreach (T obj in entities)
                dbSet.Remove(obj);
        }

        /// <summary>
        /// Implement get entity by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Implement get all entity
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        /// <summary>
        /// Implement entities
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace AirLines.Core.Repository
{
    public interface IEfRepositoryBase<TEntity>
       where TEntity : class
    {
        #region Methods
        TEntity GetById(int key);
        Task<TEntity> GetByIdAsync(int key);
        IEnumerable<TEntity> Get();
        Task<IEnumerable<TEntity>> GetAsync();
        bool Insert(TEntity entity);
        Task<bool> InsertAsync(TEntity entity);
        bool Update(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        bool SaveChanges();
        Task<bool> SaveChangesAsync();      

        #endregion
    }

public class EfRepositoryBase<TEntity>
                : IEfRepositoryBase<TEntity>

        //: IEfRepository<TKey, TEntity>
        //, IEfRepositoryAsync<TKey, TEntity>
        where TEntity : class
    {
        #region Fields
        internal DbContext context;
        internal DbSet<TEntity> entity;
        //  private bool AutoSave;
        protected Boolean Disponsed;
        #endregion

        #region Propeties
        /// <summary>
        /// Entities
        /// </summary>
        public virtual DbSet<TEntity> Entity
        {
            get
            {
                if (entity == null)
                    entity = context.Set<TEntity>();           

                return entity;
            }
        }

        public virtual DbContext Context
        {
            get
            {
                return context;
            }
        }
        #endregion

        #region Ctor              
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepositoryBase(DbContext context) //: this(context, false)
        {
            this.context = context;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="key">Identifier</param>
        /// <returns>Entity</returns>
        public virtual TEntity GetById(int key)
        {
            return this.Entity.Find(key);
        }

     
        public virtual async Task<TEntity> GetByIdAsync(int key)
        {
            return await this.Entity.FindAsync(key);
        }


        public virtual IEnumerable<TEntity> Get()
        {
            return this.Entity.AsEnumerable();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await this.Entity.ToArrayAsync();
        }


        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual bool Insert(TEntity entity)
        {          

            this.Entity.Add(entity);

            return this.SaveChanges();
        }


        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<bool> InsertAsync(TEntity entity)
        {            

            await this.Entity.AddAsync(entity);

            return await this.SaveChangesAsync();
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual bool Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;

            return this.SaveChanges();
        }

        // <summary>
        /// Update Async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;

            return await this.SaveChangesAsync();
        }


        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual bool Delete(TEntity entity)
        {
            this.entity.Remove(entity);

            return this.SaveChanges();
        }

   

        // <summary>
        /// Update Async entity
        /// </summary>
        /// <param name="entity">Entity</param>
        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            this.entity.Remove(entity);

            return await this.SaveChangesAsync();
        }

        #endregion
        #region Commit
        public bool SaveChanges()
        {
            return this.context.SaveChanges() > 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        #endregion
    }
}
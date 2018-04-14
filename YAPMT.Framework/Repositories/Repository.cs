using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YAPMT.Framework.Entities;
using YAPMT.Framework.Specifications;

namespace YAPMT.Framework.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(PrincipalDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        public virtual Task CommitAsync()
        {
            return this.dbContext.SaveChangesAsync();
        }

        public virtual Task<long> CountAsync()
        {
            return this.Query().LongCountAsync();
        }

        public virtual Task<long> CountAsync(BaseSpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            return this.Query().LongCountAsync(specification.Expression);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Task.Run(() => this.dbSet.Remove(entity));
        }

        public virtual Task<bool> ExistsAsync(BaseSpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            return this.Query().AnyAsync(specification.Expression);
        }

        public virtual Task<TEntity> GetAsync(params object[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));

            return this.dbSet.FindAsync(keys);
        }

        public virtual Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return this.dbSet.AddAsync(entity);
        }

        public virtual Task<List<TEntity>> QueryAsync(BaseSpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            return this.Query().Where(specification.Expression).ToListAsync();
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return Task.Run(() => this.dbSet.Update(entity));
        }

        protected IQueryable<TEntity> QueryAsTracking()
        {
            return this.dbSet;
        }

        protected IQueryable<TEntity> Query()
        {
            return this.dbSet.AsNoTracking();
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return this.Query().ToListAsync();
        }
    }
}

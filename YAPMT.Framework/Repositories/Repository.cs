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

        public async virtual Task CommitAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }

        public async virtual Task<long> CountAsync()
        {
            return await this.Query().LongCountAsync();
        }

        public async virtual Task<long> CountAsync(BaseSpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            return await this.Query().LongCountAsync(specification.Expression);
        }

        public async virtual Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await Task.Run(() => this.dbSet.Remove(entity));
        }

        public async virtual Task<bool> ExistsAsync(BaseSpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            return await this.Query().AnyAsync(specification.Expression);
        }

        public async virtual Task<TEntity> GetAsync(params object[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));

            return await this.dbSet.FindAsync(keys);
        }

        public async virtual Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await this.dbSet.AddAsync(entity);
        }

        public async virtual Task<List<TEntity>> QueryAsync(BaseSpecification<TEntity> specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            return await this.Query().Where(specification.Expression).ToListAsync();
        }

        public async virtual Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await  Task.Run(() => this.dbSet.Update(entity));
        }

        protected IQueryable<TEntity> QueryAsTracking()
        {
            return this.dbSet;
        }

        protected IQueryable<TEntity> Query()
        {
            return this.dbSet.AsNoTracking();
        }

        public async virtual Task<List<TEntity>> GetAllAsync()
        {
            return await this.Query().ToListAsync();
        }
    }
}

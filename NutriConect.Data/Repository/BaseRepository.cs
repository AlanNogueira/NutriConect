using Microsoft.EntityFrameworkCore;
using NutriConect.Business.Entities;
using NutriConect.Business.Interfaces.Repository;
using NutriConect.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly ContextBase Db;
        protected readonly DbSet<TEntity> DbSet;
        public BaseRepository(ContextBase db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> expression)
        {
            return await DbSet.AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> FindById(int id)
        {
            return await DbSet.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveUpdates();
        }

        public async Task AddList(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
            await SaveUpdates();
        }
        public async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveUpdates();
        }

        public async Task UpdateList(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
            await SaveUpdates();
        }

        public async Task Delete(int id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveUpdates();
        }

        public async Task DeleteList(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            await SaveUpdates();
        }

        public async Task<int> SaveUpdates()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}

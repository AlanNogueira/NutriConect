using NutriConect.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NutriConect.Business.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task Add(TEntity entity);
        Task AddList(IEnumerable<TEntity> entities);
        Task<TEntity> FindById(int id);
        Task<IEnumerable<TEntity>> FindAll();
        Task Update(TEntity entity);
        Task UpdateList(IEnumerable<TEntity> entities);
        Task Delete(int id);
        Task DeleteList(IEnumerable<TEntity> entities);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> expression);
        Task<int> SaveUpdates();
    }
}

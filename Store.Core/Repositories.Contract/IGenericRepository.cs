using Store.Core.Entities;
using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(TKey id);

        Task AddAsync(TEntity entity);

        void UpdateAsync(TEntity entity);

        void DeleteAsync(TEntity entity);

        // With Specifications :

        Task<IEnumerable<TEntity>> GetAllWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications);

        Task<TEntity> GetByIdWithSpecificationsAsync(ISpecifications<TEntity, TKey> specifications);
    }

}

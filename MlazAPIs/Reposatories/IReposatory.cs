using System.Linq.Expressions;

namespace MlazAPIs.Reposatories
{
    public interface IReposatory<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync
            (
            Expression<Func<TEntity, bool>>? expression = null,
            Expression<Func<TEntity, object>>[]? includes = null,
            bool tracking = true,
            CancellationToken cancellationToken = default
            );
        Task<TEntity> GetOneAsync
            (
            Expression<Func<TEntity , bool>>? expression = null,
            Expression<Func<TEntity , object>>[]? includes = null,
            bool tracking = true,
            CancellationToken cancellationToken = default
            );
        Task AddAsync(TEntity entity , CancellationToken cancellationToken = default);
        void Update(TEntity entity , CancellationToken cancellationToken = default);
        void Delete(TEntity entity , CancellationToken cancellationToken = default);
        Task CommitAsync(CancellationToken cancellationToken = default);

    }
}

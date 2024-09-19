using AuthenticationAndAuthorization.Infrastructure.Helpers;
using System.Linq.Expressions;

namespace AuthenticationAndAuthorization.Infrastructure.Repositories.Interfaces
{
    public interface IEntityRepository<TEntity> where TEntity : class
    {
        #region Commands
        Task<int> CommandAsync(TEntity entity, CommandMode commandMode, CancellationToken cancellationToken);
        #endregion

        #region Queries
        Task<List<TEntity>> ToListAsync { get; }

        ValueTask<TEntity?> FindAsync(params object?[]? keyValues);

        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Where(Func<TEntity, bool> predicate);
        #endregion
    }
}
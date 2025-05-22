using System.Linq.Expressions;

namespace ProductManagementCatalogue.Domain.Shared.Interfaces;

public interface IRepository<T>
{
	Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);

	Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

	Task<T[]> QueryAsync(Expression<Func<T, bool>> predicate, int pageSize = 10, int pageNumber = 1, CancellationToken cancellationToken = default);

	Task AddAsync(T entity, CancellationToken cancellationToken = default);

	Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

	Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}
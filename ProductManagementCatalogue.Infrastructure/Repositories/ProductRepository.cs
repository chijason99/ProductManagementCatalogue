using ProductManagementCatalogue.Domain.Products;
using ProductManagementCatalogue.Domain.Shared.Interfaces;
using ProductManagementCatalogue.Infrastructure.DbContexts;
using System.Linq.Expressions;

namespace ProductManagementCatalogue.Infrastructure.Repositories;

public class ProductRepository(ProductDbContext productDbContext) : IRepository<Product>
{
	private readonly ProductDbContext _context = productDbContext;

	public async Task AddAsync(Product entity, CancellationToken cancellationToken = default)
	{
		await _context.Set<Product>().AddAsync(entity, cancellationToken);
	}

	public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
	{
		Product entity = await GetByIdAsync(id, cancellationToken);

		_context.Set<Product>().Remove(entity);
	}

	public Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default)
	{
		Product product = await _context.Products.FindAsync(id, cancellationToken);

		return product;
	}

	public Task<IEnumerable<Product>> QueryAsync(Expression<Func<Product, bool>> predicate, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task UpdateAsync(Product entity, CancellationToken cancellationToken = default)
	{
		_context.Products.Update(entity);

		return Task.CompletedTask;
	}
}

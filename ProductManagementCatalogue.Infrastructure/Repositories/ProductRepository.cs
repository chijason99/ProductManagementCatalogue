using Microsoft.EntityFrameworkCore;
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

	public Task DeleteAsync(Product entity, CancellationToken cancellationToken = default)
	{
		_context.Set<Product>().Remove(entity);

		return Task.CompletedTask;
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

	public async Task<Product[]> QueryAsync(
		Expression<Func<Product, bool>> predicate, 
		int pageNumber = 1, 
		int pageSize = 10,
		CancellationToken cancellationToken = default)
	{
		var result = await _context.Products
			.AsNoTracking()
			.Where(predicate)
			.Skip(pageSize * (pageNumber - 1))
			.Take(pageSize)
			.ToArrayAsync(cancellationToken);

		return result;
	}

	public Task UpdateAsync(Product entity, CancellationToken cancellationToken = default)
	{
		_context.Products.Update(entity);

		return Task.CompletedTask;
	}
}

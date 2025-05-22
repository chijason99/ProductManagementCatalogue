using Microsoft.EntityFrameworkCore;
using ProductManagementCatalogue.Domain.Products;
using ProductManagementCatalogue.Domain.Shared.Interfaces;

namespace ProductManagementCatalogue.Infrastructure.DbContexts;

public class ProductDbContext : DbContext, IUnitOfWork
{

	//  dotnet ef migrations add <migration_name> --startup-project ProductManagementCatalogue.UI --project ProductManagementCatalogue.Infrastructure
	//  dotnet ef database update --startup-project ProductManagementCatalogue.UI --project ProductManagementCatalogue.Infrastructure
	public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
	{
	}

	public DbSet<Product> Products { get; set; }
}

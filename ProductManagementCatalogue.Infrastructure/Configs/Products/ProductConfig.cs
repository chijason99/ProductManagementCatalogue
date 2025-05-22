using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagementCatalogue.Domain.Products;

namespace ProductManagementCatalogue.Infrastructure.Configs.Products;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
	public void Configure(EntityTypeBuilder<Product> builder)
	{
		builder.Property(p => p.Id)
			.ValueGeneratedOnAdd();

		builder.Property(p => p.Name)
			.IsRequired()
			.HasMaxLength(250);

		builder.Property(p => p.Description)
			.IsRequired()
			.HasMaxLength(1000);
	}
}
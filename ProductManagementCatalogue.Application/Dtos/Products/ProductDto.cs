using System.ComponentModel.DataAnnotations;

namespace ProductManagementCatalogue.Application.Dtos.Products;

public record ProductDto
{
	public int Id { get; set; }
	
	[Required, MaxLength(250)]
	public string Name { get; set; }

	[Required, MaxLength(250)]
	public string Description { get; set; }

	[Required, Range(0.01, int.MaxValue)]
	public decimal Price { get; set; }

	[Required, Range(0.0, int.MaxValue)]
	public int Stock { get; set; }

	public bool IsActive { get; set; }
}
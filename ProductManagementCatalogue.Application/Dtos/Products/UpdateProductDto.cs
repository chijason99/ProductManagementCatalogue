using System.ComponentModel.DataAnnotations;

namespace ProductManagementCatalogue.Application.Dtos.Products;

public class UpdateProductDto
{
	[Required, MaxLength(250)]
	public string Name { get; set; }

	[Required, MaxLength(1000)]
	public string Description { get; set; }

	[Range(0.01, int.MaxValue)]
	public decimal Price { get; set; }

	[Range(0, int.MaxValue)]
	public int Stock { get; set; }

	public bool IsActive { get; set; }
}
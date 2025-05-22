using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductManagementCatalogue.Application.Dtos.Products;

public record CreateProductDto
{
	[JsonPropertyName("name")]
	[Required, MaxLength(250)]
	public string Name { get; init; }

	[JsonPropertyName("description")]
	[Required, MaxLength(1000)]
	public string Description { get; init; }

	[JsonPropertyName("price")]
	[Required, Range(0.01, int.MaxValue)]
	public decimal Price { get; init; }

	[JsonPropertyName("stock")]
	[Required, Range(0, (double)int.MaxValue)]
	public int Stock { get; init; }

	[JsonPropertyName("isActive")]
	public bool IsActive { get; init; }
}

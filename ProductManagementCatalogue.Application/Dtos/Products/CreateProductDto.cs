using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductManagementCatalogue.Application.Dtos.Products;

public record CreateProductDto
{
	[JsonPropertyName("name")]
	[Required, StringLength(250, MinimumLength = 0)]
	public string Name { get; init; }

	[JsonPropertyName("description")]
	[Required, StringLength(250, MinimumLength = 0)]
	public string Description { get; init; }

	[JsonPropertyName("price")]
	[Required, Range(0, (double)int.MaxValue)]
	public decimal Price { get; init; }

	[JsonPropertyName("stock")]
	[Required, Range(0, (double)int.MaxValue)]
	public int Stock { get; init; }

	[JsonPropertyName("isActive")]
	public bool IsActive { get; init; }
}

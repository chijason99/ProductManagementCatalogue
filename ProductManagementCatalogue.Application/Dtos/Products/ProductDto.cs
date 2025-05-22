namespace ProductManagementCatalogue.Application.Dtos.Products;

public record ProductDto
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int Stock { get; set; }
	public bool IsActive { get; set; }
}
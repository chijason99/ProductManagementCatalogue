namespace ProductManagementCatalogue.Application.Dtos.Products;

public class ProductSearchFilter
{
	public string? SearchString { get; set; }

	public bool? IsActive { get; set; }

	public decimal? MinPrice { get; set; }

	public decimal? MaxPrice { get; set; }

	public int? MinStock { get; set; }

	public int? MaxStock { get; set; }
}
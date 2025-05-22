using ProductManagementCatalogue.Application.Dtos.Products;
using ProductManagementCatalogue.Application.Dtos.Shared;

namespace ProductManagementCatalogue.Application.ViewModels;

public class ProductIndexViewModel
{
	public PagedResult<ProductDto> Products { get; set; }

	public ProductSearchFilter CurrentFilter { get; set; }

	public PagedQueryMetaData CurrentPaginationData { get; set; }
}
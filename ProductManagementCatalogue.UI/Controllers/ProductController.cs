using Microsoft.AspNetCore.Mvc;
using ProductManagementCatalogue.Application.Dtos.Products;
using ProductManagementCatalogue.Application.Dtos.Shared;
using ProductManagementCatalogue.Application.Services;
using ProductManagementCatalogue.Application.ViewModels;

namespace ProductManagementCatalogue.UI.Controllers;

[Controller]
public class ProductController(IProductService productService) : Controller
{
	private readonly IProductService _productService = productService;

	[HttpGet]
	public async Task<IActionResult> Index(
		ProductSearchFilter filter,
		PagedQueryMetaData pagedQueryMetaData,
		CancellationToken cancellationToken = default)
	{
		var getProductsResult = await _productService.GetAllProductsAsync(
				filter,
				pagedQueryMetaData,
				cancellationToken
			);

		return View(new ProductIndexViewModel()
		{
			Products = getProductsResult.Value,
			CurrentFilter = filter,
			CurrentPaginationData = pagedQueryMetaData
		});
	}
}

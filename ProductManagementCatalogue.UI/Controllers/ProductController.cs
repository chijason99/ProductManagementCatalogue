using Microsoft.AspNetCore.Mvc;
using ProductManagementCatalogue.Application.Dtos.Products;
using ProductManagementCatalogue.Application.Dtos.Shared;
using ProductManagementCatalogue.Application.Services;
using ProductManagementCatalogue.Application.ViewModels;
using ProductManagementCatalogue.Domain.Shared.Results;

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

	[HttpGet("[action]/{id:int}")]
	public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
	{
		Result<ProductDto> getProductResult = await _productService.GetProductAsync(id, cancellationToken);

		if (getProductResult.IsFailure)
			return BadRequest();

		return View(getProductResult.Value);
	}

	[HttpGet]
	public IActionResult Create()
	{
		CreateProductDto dto = new()
		{
			IsActive = true
		};

		return View(dto);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(CreateProductDto dto, CancellationToken cancellationToken)
	{
		if (!ModelState.IsValid)
			return View(dto);

		var createProductResult = await _productService.CreateProductAsync(dto, cancellationToken);

		return RedirectToAction(nameof(Details), new { id = createProductResult.Value.Id });
	}

	[HttpGet("[action]/{id:int}")]
	public async Task<IActionResult> Update(int id, CancellationToken cancellationToken)
	{
		Result<ProductDto> getProductResult = await _productService.GetProductAsync(id, cancellationToken);

		if (getProductResult.IsFailure)
			return BadRequest();

		UpdateProductDto dto = new()
		{
			Description = getProductResult.Value.Description,
			Name = getProductResult.Value.Name,
			Stock = getProductResult.Value.Stock,
			Price = getProductResult.Value.Price,
			IsActive = getProductResult.Value.IsActive
		};

		return View(getProductResult.Value);
	}

	[HttpPost("[action]")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> UpdateProductAsync(int id, ProductDto productDto, CancellationToken cancellationToken)
	{
		if (!ModelState.IsValid)
			return View(productDto);

		UpdateProductDto dto = new()
		{
			Description = productDto.Description,
			Name = productDto.Name,
			Price = productDto.Price,
			IsActive = productDto.IsActive,
			Stock = productDto.Stock,
		};

		Result updateProductResult = await _productService.UpdateProductAsync(id, dto, cancellationToken);

		if (updateProductResult.IsFailure)
		{
			foreach (var error in updateProductResult.Errors)
				ModelState.AddModelError(string.Empty, error);

			return View(productDto);
		}

		return RedirectToAction(nameof(Details), new { id = id });
	}


	[HttpGet("[action]")]
	public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
	{
		Result deleteProductResult = await _productService.DeleteProductAsync(id, cancellationToken);

		if (deleteProductResult.IsFailure)
		{
			return RedirectToAction(nameof(Index));
		}

		return RedirectToAction(nameof(Index), new { id = id });
	}
}
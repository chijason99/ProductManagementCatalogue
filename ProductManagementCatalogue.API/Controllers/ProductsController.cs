using Microsoft.AspNetCore.Mvc;
using ProductManagementCatalogue.Application.Dtos.Products;
using ProductManagementCatalogue.Application.Services;
using ProductManagementCatalogue.Domain.Shared.Results;

namespace ProductManagementCatalogue.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
	private readonly IProductService _productService = productService;

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDto))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<string>))]
	public async Task<IActionResult> CreateAsync(CreateProductDto productDto, CancellationToken cancellationToken = default)
	{
		Result<ProductDto> createProductResult = await _productService.CreateProductAsync(productDto, cancellationToken);

		if (createProductResult.IsFailure)
			return BadRequest();

		return Created();
	}
}
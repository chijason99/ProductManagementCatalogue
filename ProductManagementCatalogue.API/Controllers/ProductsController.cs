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
	public async Task<IActionResult> CreateAsync(
		[FromBody] CreateProductDto productDto, 
		CancellationToken cancellationToken = default)
	{
		Result<ProductDto> createProductResult = await _productService.CreateProductAsync(productDto, cancellationToken);

		if (createProductResult.IsFailure)
			return BadRequest();

		return Created();
	}

	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
	public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken = default)
	{
		Result<ProductDto> getProductResult = await _productService.GetProductAsync(id, cancellationToken);

		if (getProductResult.IsFailure)
			return BadRequest();

		return Ok(getProductResult.Value);
	}

	[HttpPatch("{id:int}")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
	[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IEnumerable<string>))]
	public async Task<IActionResult> UpdateAsync(
		int id, 
		[FromBody] UpdateProductDto updateProductDto, 
		CancellationToken cancellationToken = default)
	{
		Result updateProductResult = await _productService.UpdateProductAsync(id, updateProductDto, cancellationToken);

		if (updateProductResult.IsFailure)
		{
			if (updateProductResult.Errors.Any(err => err.Contains("Not Found")))
				return NotFound();

			return BadRequest();
		}

		return NoContent();
	}
}
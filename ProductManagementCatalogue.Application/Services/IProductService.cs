using ProductManagementCatalogue.Application.Dtos.Products;
using ProductManagementCatalogue.Domain.Products;
using ProductManagementCatalogue.Domain.Shared.Results;

namespace ProductManagementCatalogue.Application.Services;

public interface IProductService
{
	Task<Result<ProductDto>> CreateProductAsync(CreateProductDto createProductDto, CancellationToken cancellationToken = default);
}
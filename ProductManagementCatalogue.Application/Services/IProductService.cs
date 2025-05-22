using ProductManagementCatalogue.Application.Dtos.Products;
using ProductManagementCatalogue.Application.Dtos.Shared;
using ProductManagementCatalogue.Domain.Shared.Results;

namespace ProductManagementCatalogue.Application.Services;

public interface IProductService
{
	Task<Result<ProductDto>> CreateProductAsync(CreateProductDto createProductDto, CancellationToken cancellationToken = default);

	Task<Result<ProductDto>> GetProductAsync(int id, CancellationToken cancellationToken = default);

	Task<Result> UpdateProductAsync(int id, UpdateProductDto updateProductDto, CancellationToken cancellationToken = default);

	Task<Result> DeleteProductAsync(int id, CancellationToken cancellationToken = default);

	Task<Result<PagedResult<ProductDto>>> GetAllProductsAsync(ProductSearchFilter filter, PagedQueryMetaData pagedMetaData, CancellationToken cancellationToken = default);
}
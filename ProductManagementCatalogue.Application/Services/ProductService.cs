using ProductManagementCatalogue.Application.Dtos.Products;
using ProductManagementCatalogue.Domain.Products;
using ProductManagementCatalogue.Domain.Shared.Interfaces;
using ProductManagementCatalogue.Domain.Shared.Results;

namespace ProductManagementCatalogue.Application.Services;

public class ProductService(IRepository<Product> productRepository, IUnitOfWork unitOfWork) : IProductService
{
	private readonly IRepository<Product> _productRepository = productRepository;
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<Result<ProductDto>> CreateProductAsync(
		CreateProductDto createProductDto,
		CancellationToken cancellationToken = default)
	{
		Result<Product> productCreateResult = Product.Create(
			createProductDto.Name, 
			createProductDto.Description, 
			createProductDto.Price, 
			createProductDto.Stock, 
			createProductDto.IsActive);

		if (productCreateResult.IsFailure)
			return Result<ProductDto>.Failure(productCreateResult.Errors);

		await _productRepository.AddAsync(productCreateResult.Value, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		ProductDto dto = new()
		{
			Id = productCreateResult.Value.Id,
			Name = createProductDto.Name,
			Description = createProductDto.Description,
			Price = createProductDto.Price,
			Stock = createProductDto.Stock,
			IsActive = createProductDto.IsActive
		};

		return Result<ProductDto>.Success(dto);
	}
}

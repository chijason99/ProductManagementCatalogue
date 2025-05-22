using LinqKit;
using ProductManagementCatalogue.Application.Dtos.Products;
using ProductManagementCatalogue.Application.Dtos.Shared;
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

	public async Task<Result<ProductDto>> GetProductAsync(int id, CancellationToken cancellationToken = default)
	{
		Product targetProduct = await _productRepository.GetByIdAsync(id, cancellationToken);

		if (targetProduct is null)
			return Result<ProductDto>.Failure($"Product not found");

		ProductDto dto = new()
		{
			Id = targetProduct.Id,
			Name = targetProduct.Name,
			Description = targetProduct.Description,
			Price = targetProduct.Price,
			Stock = targetProduct.Stock,
			IsActive = targetProduct.IsActive
		};

		return Result<ProductDto>.Success(dto);
	}

	public async Task<Result> UpdateProductAsync(int id, UpdateProductDto updateProductDto, CancellationToken cancellationToken = default)
	{
		Product targetProduct = await _productRepository.GetByIdAsync(id, cancellationToken);

		if (targetProduct is null)
			return Result.Failure($"Product not found");

		targetProduct.UpdateDetails(
			updateProductDto.Name,
			updateProductDto.Description,
			updateProductDto.Price,
			updateProductDto.Stock,
			updateProductDto.IsActive);

		await _productRepository.UpdateAsync(targetProduct, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}

	public async Task<Result> DeleteProductAsync(int id, CancellationToken cancellationToken = default)
	{
		Product targetProduct = await _productRepository.GetByIdAsync(id, cancellationToken);

		if (targetProduct is null)
			return Result.Failure($"Product not found");

		await _productRepository.DeleteAsync(targetProduct, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}

	public async Task<Result<PagedResult<ProductDto>>> GetAllProductsAsync(
			ProductSearchFilter filter,
			PagedQueryMetaData pagedMetaData,
			CancellationToken cancellationToken = default)
	{
		int pageNumber = pagedMetaData.PageNumber < 1 ? 1 : pagedMetaData.PageNumber;
		int pageSize = pagedMetaData.PageSize < 1 ? 10 : pagedMetaData.PageSize;

		var searchPredicate = BuildProduceSearchClause(filter);

		var products = await _productRepository.QueryAsync(searchPredicate, pageNumber, pageSize, cancellationToken);

		var productDtos = products.Select(p => new ProductDto
		{
			Id = p.Id,
			Name = p.Name,
			Description = p.Description,
			Price = p.Price,
			Stock = p.Stock,
			IsActive = p.IsActive
		}).ToList();

		var pagedResult = PagedResult<ProductDto>.Create(
			productDtos, 
			totalItems: products.Length,
			pageNumber, 
			pageSize);

		return Result<PagedResult<ProductDto>>.Success(pagedResult);
	}

	private ExpressionStarter<Product> BuildProduceSearchClause(ProductSearchFilter filter)
	{
		var predicate = PredicateBuilder.New<Product>(true);

		if (!string.IsNullOrWhiteSpace(filter.SearchString))
		{
			string searchLower = filter.SearchString.ToLower();
			predicate = predicate.And(p => p.Name.ToLower().Contains(searchLower));
		}

		if (filter.IsActive.HasValue)
			predicate = predicate.And(p => p.IsActive == filter.IsActive.Value);

		if (filter.MinPrice.HasValue)
			predicate = predicate.And(p => p.Price >= filter.MinPrice.Value);

		if (filter.MaxPrice.HasValue)
			predicate = predicate.And(p => p.Price <= filter.MaxPrice.Value);

		if (filter.MinStock.HasValue)
			predicate = predicate.And(p => p.Stock >= filter.MinStock.Value);

		if (filter.MaxStock.HasValue)
			predicate = predicate.And(p => p.Stock <= filter.MaxStock.Value);

		return predicate;
	}
}

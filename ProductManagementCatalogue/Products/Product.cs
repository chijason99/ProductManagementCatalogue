using ProductManagementCatalogue.Domain.Shared.Results;

namespace ProductManagementCatalogue.Domain.Products;

public class Product
{
	// EF
	protected Product() { }

	public static Result<Product> Create(string name, string description, decimal price, int stock, bool isActive)
	{
		List<string> errors = [];

		if (string.IsNullOrWhiteSpace(name)) 
			errors.Add("Product name cannot be empty.");

		if (string.IsNullOrWhiteSpace(description))
			errors.Add("Product description cannot be empty.");

		if (price <= 0) 
			errors.Add("Price must be a positive value.");

		if (stock <= 0)
			errors.Add("Stock must be a positive value.");

		if (errors.Any())
			return Result<Product>.Failure(errors);

		return Result<Product>.Success(new Product 
		{ 
			Name = name,
			Description = description,
			Price = price,
			Stock = stock,
			IsActive = isActive
		});
	}

	public int Id { get; private set; }

	public string Name { get; private set; }

	public string Description { get; private set; }

	public decimal Price { get; private set; }

	public int Stock { get; private set; }

	public bool IsActive { get; private set; }

	public Result UpdateDetails(
		string name, 
		string description, 
		decimal price, 
		int stock, 
		bool isActive)
	{
		List<string> errors = [];

		if (string.IsNullOrWhiteSpace(name)) 
			errors.Add("Product name cannot be empty.");

		if (string.IsNullOrWhiteSpace(description)) 
			errors.Add("Product description cannot be empty.");

		if (price <= 0) 
			errors.Add("Price must be a positive value.");

		if (stock <= 0) 
			errors.Add("Stock must be a positive value.");

		if (errors.Any()) 
			return Result.Failure(errors);

		Name = name;
		Description = description;
		Price = price;
		Stock = stock;
		IsActive = isActive;

		return Result.Success();
	}
}
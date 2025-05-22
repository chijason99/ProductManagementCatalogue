namespace ProductManagementCatalogue.Application.Dtos.Shared;

public class PagedResult<T>(List<T> items, int totalItems, int pageNumber, int pageSize)
{
	public List<T> Items { get; } = items;
	public int PageNumber { get; } = pageNumber;
	public int PageSize { get; } = pageSize;
	public int TotalItems { get; } = totalItems;
	public int TotalPages { get; } = (int)Math.Ceiling(totalItems / (double)pageSize);

	public bool HasPreviousPage => PageNumber > 1;
	public bool HasNextPage => PageNumber < TotalPages;

	public static PagedResult<T> Create(List<T> items, int totalItems, int pageNumber, int pageSize)	
		=> new(items, totalItems, pageNumber, pageSize);
}
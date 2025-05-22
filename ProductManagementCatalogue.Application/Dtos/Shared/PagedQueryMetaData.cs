namespace ProductManagementCatalogue.Application.Dtos.Shared;

public class PagedQueryMetaData
{
	public int PageNumber { get; set; } = 1;

	public int PageSize { get; set; } = 10;
}
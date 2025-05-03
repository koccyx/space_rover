namespace Application.Models.Queries.Folder;

public sealed record GetFoldersQuery
{
	public required Guid CompanyId { get; init; }
}
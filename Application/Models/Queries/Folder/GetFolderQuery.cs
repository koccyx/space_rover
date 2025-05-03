namespace Application.Models.Queries.Folder;

public sealed record GetFolderQuery
{
	public Guid Id { get; init; }
}
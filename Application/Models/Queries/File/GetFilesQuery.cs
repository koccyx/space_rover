namespace Application.Models.Queries.File;

public sealed record GetFilesQuery
{
	public required Guid FolderId { get; init; }
}
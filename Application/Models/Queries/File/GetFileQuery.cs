namespace Application.Models.Queries.File;

public sealed record GetFileQuery
{
	public Guid Id { get; init; }
}
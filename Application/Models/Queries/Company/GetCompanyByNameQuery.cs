namespace Application.Models.Queries;

public sealed record GetCompanyByNameQuery
{
	public string Name { get; init; }
}
namespace Application.Models.Queries.Company;

public sealed record GetCompanyQuery
{
	public Guid Id { get; init; }
}
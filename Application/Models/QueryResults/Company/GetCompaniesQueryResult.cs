namespace Application.Models.QueryResults.Company;

public class GetCompaniesQueryResult
{
	public required IReadOnlyCollection<Models.Company> Companies { get; init; }
	
	public required int TotalCount { get; init; } 
}
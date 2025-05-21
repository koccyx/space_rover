namespace Application.Models.QueryResults.User;

public sealed record GetUserCompanyQueryResult
{
	public required Models.Company Company { get; init; }
}
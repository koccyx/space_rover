namespace Application.Models.QueryResults.User;

public sealed record LoginUserQueryResult
{
	public string Token { get; init; }
}
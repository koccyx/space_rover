namespace Application.Models.QueryResults.User;

public sealed record RegisterUserQueryResult
{
	public string Token { get; init; }
}
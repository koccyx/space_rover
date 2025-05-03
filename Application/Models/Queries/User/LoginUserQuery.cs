namespace Application.Models.QueryResults.User;

public sealed record LoginUserQuery
{
	public string Name { get; init; }
	
	public string Password { get; init; }
}
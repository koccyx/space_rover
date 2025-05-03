namespace Application.Models.QueryResults.User;

public class GetUsersQueryResult
{
	public required IReadOnlyCollection<Models.User> Users { get; init; }
	
	public required int TotalCount { get; init; } 
}
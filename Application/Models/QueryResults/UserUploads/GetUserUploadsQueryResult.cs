namespace Application.Models.QueryResults.UserUploads;

public class GetUserUploadsQueryResult
{
	public required IReadOnlyCollection<Models.UserUploads> UserUploads { get; init; }
	public required int TotalCount { get; init; } 	
}
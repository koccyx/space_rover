namespace Application.Models.QueryResults.FileDetails;

public class GetFileDetailsQueryResult
{
	public required IReadOnlyCollection<Models.FileDetails> FileDetails { get; init; }
	public required int TotalCount { get; init; } 	
}
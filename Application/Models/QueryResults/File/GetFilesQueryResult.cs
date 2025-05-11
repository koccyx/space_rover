namespace Application.Models.QueryResults.File;

public class GetFilesQueryResult
{
	public required IReadOnlyCollection<Models.File> Files { get; init; }
	
	public required int TotalCount { get; init; } 
}
namespace Application.Models.QueryResults.Folder;

public class GetFoldersQueryResult
{
	public required IReadOnlyCollection<Models.Folder> Folders { get; init; }
	
	public required int TotalCount { get; init; } 
}
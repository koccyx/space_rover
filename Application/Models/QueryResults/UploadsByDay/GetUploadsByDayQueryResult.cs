using Model.Models;

namespace Application.Models.QueryResults.UploadsByDay;

public class GetUploadsByDayQueryResult
{
	public required IReadOnlyCollection<UploadsByDayStatistics> UploadsByDay { get; init; }
	public required int TotalCount { get; init; } 	
}
namespace space_rovers.Models.Statistics.Responses;

public sealed record GetUploadsByDayStatisticsResponse
{
	public required IReadOnlyCollection<Application.Models.UploadsByDayStatistics> UploadsByDayStatistics { get; init; }

	public required int TotalCount { get; init; }
}

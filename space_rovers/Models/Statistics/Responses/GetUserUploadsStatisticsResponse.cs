namespace space_rovers.Models.Statistics.Responses;

public sealed record GetUsersUploadsStatisticsResponse
{
	public required IReadOnlyCollection<Application.Models.UserUploads> UserUploads { get; init; }

	public required int TotalCount { get; init; }
}

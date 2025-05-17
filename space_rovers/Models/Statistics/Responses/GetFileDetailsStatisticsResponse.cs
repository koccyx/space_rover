namespace space_rovers.Models.Statistics.Responses;

public sealed record GetFileDetailsStatisticsResponse
{
	/// <summary>
	/// Пользователи
	/// </summary>
	public required IReadOnlyCollection<Application.Models.FileDetails> FileDetails { get; init; }

	/// <summary>
	/// Общее количество записей
	/// </summary>
	public required int TotalCount { get; init; }
}

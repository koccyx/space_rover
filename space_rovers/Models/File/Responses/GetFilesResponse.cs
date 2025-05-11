namespace space_rovers.Models.File.Responses;

/// <summary>
/// Ответ на запрос на получение списка директорий
/// </summary>
public sealed record GetFilesResponse
{
	/// <summary>
	/// Пользователи
	/// </summary>
	public required IReadOnlyCollection<Application.Models.File> Files { get; init; }

	/// <summary>
	/// Общее количество записей
	/// </summary>
	public required int TotalCount { get; init; }
}

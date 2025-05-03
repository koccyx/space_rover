namespace space_rovers.Models.Folder.Responses;

/// <summary>
/// Ответ на запрос на получение списка директорий
/// </summary>
public sealed record GetFoldersResponse
{
	/// <summary>
	/// Пользователи
	/// </summary>
	public required IReadOnlyCollection<Application.Models.Folder> Folders { get; init; }

	/// <summary>
	/// Общее количество записей
	/// </summary>
	public required int TotalCount { get; init; }
}

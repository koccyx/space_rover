namespace space_rovers.Models.User.Responses;

/// <summary>
/// Ответ на запрос на получение списка пользователей
/// </summary>
public sealed record GetUsersResponse
{
	/// <summary>
	/// Пользователи
	/// </summary>
	public required IReadOnlyCollection<Application.Models.User> Users { get; init; }

	/// <summary>
	/// Общее количество записей
	/// </summary>
	public required int TotalCount { get; init; }
}

namespace space_rovers.Models.Company.Responses;

/// <summary>
/// Ответ на запрос на получение списка компаний
/// </summary>
public sealed record GetCompaniesResponse
{
	/// <summary>
	/// Компании
	/// </summary>
	public required IReadOnlyCollection<Application.Models.Company> Companies { get; init; }

	/// <summary>
	/// Общее количество записей
	/// </summary>
	public required int TotalCount { get; init; }
}

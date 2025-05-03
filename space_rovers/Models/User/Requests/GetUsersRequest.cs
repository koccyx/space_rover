using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.Companies.Requests.User;

/// <summary>
/// Запрос на получение списка пользователей
/// </summary>
public sealed record GetUsersRequest
{
	[FromQuery]
	public int? SortField { get; init; }	
}
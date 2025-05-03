using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.Company.Requests;

/// <summary>
/// Запрос на получение списка компаний
/// </summary>
public sealed record GetCompaniesRequest
{
	[FromQuery]
	public int? SortField { get; init; }	
}
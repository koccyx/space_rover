using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.Folder.Requests;

/// <summary>
/// Запрос на получение списка дирректорий
/// </summary>
public sealed record GetFoldersRequest
{
	[FromQuery(Name = "companyId")]
	public required Guid CompanyId { get; set; }
	
	[FromQuery]
	public int? SortField { get; init; }	
}
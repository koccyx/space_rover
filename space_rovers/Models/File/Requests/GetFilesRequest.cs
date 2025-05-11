using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.File.Requests;

/// <summary>
/// Запрос на получение списка дирректорий
/// </summary>
public sealed record GetFilesRequest
{
	[FromQuery(Name = "FolderId")]
	public required Guid FolderId { get; set; }
	
	[FromQuery]
	public int? SortField { get; init; }	
}
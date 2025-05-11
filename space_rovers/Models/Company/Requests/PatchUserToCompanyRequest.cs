using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.Company.Requests;

/// <summary>
/// Добавление юзера к компании
/// </summary>
public sealed record PatchUserToCompanyRequest
{
	[FromRoute(Name = "id")] 
	public required Guid CompanyId { get; init; }
	
	[FromBody]
	public required Guid User { get; init; }
}
using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.Company.Requests;

/// <summary>
/// Добавление юзера к компании
/// </summary>
public sealed record PatchUserToCompanyRequest
{
	[FromRoute(Name = "userId")] 
	public required Guid UserId { get; init; }
}

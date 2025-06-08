using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.User.Requests;
/// <summary>
/// Запрос на получение компании
/// </summary>
public sealed record GetCompanyByUserIdRequest
{
	///<summary>
	///Идентефикатор
	///</summary>

	[FromRoute(Name = "id")]
	public required Guid Id { get; init; }

}
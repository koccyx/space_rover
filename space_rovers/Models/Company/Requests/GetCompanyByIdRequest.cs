using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.Company.Requests;
/// <summary>
/// Запрос на получение компании
/// </summary>
public sealed record GetCompanyByIdRequest
{
	///<summary>
	///Идентефикатор
	///</summary>

	[FromRoute(Name = "id")]
	public required Guid Id { get; init; }

}
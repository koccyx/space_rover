using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.Companies.Requests.User;

///<summary>
///Получение пользователя
///</summary>
public sealed record GetUserByIdRequest
{
	///<summary>
	///Идентефикатор
	///</summary>

	[FromRoute(Name = "id")]
	public required Guid Id { get; init; }
}
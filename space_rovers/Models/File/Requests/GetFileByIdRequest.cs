using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.File.Requests;

///<summary>
///Получение файла
///</summary>
public sealed record GetFileByIdRequest
{
	///<summary>
	///Идентефикатор
	///</summary>
	[FromRoute(Name = "id")]
	public required Guid Id { get; init; }
}
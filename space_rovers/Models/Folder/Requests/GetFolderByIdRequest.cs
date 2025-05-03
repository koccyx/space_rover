using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.Folder.Requests;

///<summary>
///Получение Дирректории
///</summary>
public sealed record GetFolderByIdRequest
{
	///<summary>
	///Идентефикатор
	///</summary>
	[FromRoute(Name = "id")]
	public required Guid Id { get; init; }
}
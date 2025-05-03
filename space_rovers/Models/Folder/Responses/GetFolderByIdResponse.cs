namespace space_rovers.Models.Folder.Responses;

public sealed record GetFolderByIdResponse
{
	///<summary>
	///Дирректория
	///</summary>
	public required Application.Models.Folder Folder { get; init; }
}
namespace space_rovers.Models.Folder.Responses;

public sealed record PostFolderResponse
{
	///<summary>
	///Директория
	///</summary>
	public required Application.Models.Folder Folder { get; init; }
}
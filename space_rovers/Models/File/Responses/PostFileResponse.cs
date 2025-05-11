namespace space_rovers.Models.File.Responses;

public sealed record PostFileResponse
{
	///<summary>
	///Директория
	///</summary>
	public required Application.Models.File File { get; init; }
}
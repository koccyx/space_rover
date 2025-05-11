namespace space_rovers.Models.File.Responses;

public sealed record GetFileByIdResponse
{
	///<summary>
	///Дирректория
	///</summary>
	public required Application.Models.File File { get; init; }
}
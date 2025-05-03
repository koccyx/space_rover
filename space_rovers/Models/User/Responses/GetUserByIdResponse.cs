namespace space_rovers.Models.User.Responses;

public sealed record GetUserByIdResponse
{
	///<summary>
	///Пользователь
	///</summary>
	public required Application.Models.User User { get; init; }
}
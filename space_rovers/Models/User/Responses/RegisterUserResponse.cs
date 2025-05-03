namespace space_rovers.Models.User.Responses;

public sealed record RegisterUserResponse
{
	///<summary>
	///Токен
	///</summary>
	public required string Token { get; init; }
}
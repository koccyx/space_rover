namespace space_rovers.Models.User.Responses;

public sealed record GetCompanyByUserIdResponse
{
	///<summary>
	///Компания
	///</summary>
	public required Application.Models.Company Company { get; init; }
}
namespace space_rovers.Models.Company.Responses;

public sealed record PostCompanyResponse
{
	///<summary>
	///Компания
	///</summary>
	public required Application.Models.Company Company { get; init; }
}
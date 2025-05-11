namespace Application.Models.Queries.Company;

public sealed record AddUserToCompanyQuery
{
	public Guid UserId { get; set; }
	
	public Guid CompanyId { get; set; }
}
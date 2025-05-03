namespace Application.Models.Queries.User;

public sealed record GetUserQuery
{
	public Guid Id { get; init; }
	
	public Guid CompanyId { get; init; }
}
namespace Application.Models.Queries.User;

public sealed record GetUserCompanyQuery
{
	public Guid UserId { get; init; }
}
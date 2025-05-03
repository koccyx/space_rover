namespace Application.Models.Queries.User;

public sealed record RegisterUserQuery
{
	public string Name { get; set; }

	public string Password { get; set; }

	public Guid? CompanyId { get; set; }
}
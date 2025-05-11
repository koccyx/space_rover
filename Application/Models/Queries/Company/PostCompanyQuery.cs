namespace Application.Models.Queries.Company;

public sealed record PostCompanyQuery
{
	public string Name { get; init; }
	
	public float StorageLimit { get; init; }

	public float UsedStorage { get; init; } = 0;
	
	public Guid UserId { get; set; }
}
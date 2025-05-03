namespace Application.Models.Queries.User;

public sealed record PostFolderQuery
{
	public string Name { get; set; }
	
	public Guid CompanyId { get; set; }
	
	public Guid UserId { get; set; }
}
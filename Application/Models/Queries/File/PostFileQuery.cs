namespace Application.Models.Queries.File;

public sealed record PostFileQuery
{
	public string Name { get; set; }
	
	public float Size { get; set; }
	
	public Guid CompanyId { get; set; }
	
	public Guid FolderId { get; set; }
	
	public Guid UserId { get; set; }
}
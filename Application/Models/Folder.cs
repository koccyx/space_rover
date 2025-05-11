namespace Application.Models;

public class Folder
{
	public Guid Id { get; set; }
	
	public string Name { get; set; }
	
	public Guid UserId { get; set; }
	
	public Guid CompanyId { get; set; }
	
	public DateTimeOffset CreatedAt { get; set; }
}
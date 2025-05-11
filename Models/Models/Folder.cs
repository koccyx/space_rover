namespace Model.Models;

public class Folder : ICreatedAt
{
	public string Name { get; set; } 
	public Guid Id { get; set; }
	
	public Guid UserId { get; set; }
	
	public Guid CompanyId { get; set; }
	
	public Company Company { get; set; }
	
	public ICollection<File> Files { get; set; }
	
	public DateTimeOffset UpdatedAt { get; set; }
	
	public DateTimeOffset CreatedAt { get; set; }
}
using Model.Models;

namespace Application.Models;

public class File
{
	public string Name { get; set; } 
	
	public Guid Id { get; set; }
	
	public Guid UserId { get; set; }
	
	public Guid FolderId { get; set; }
	
	public DateTimeOffset UpdatedAt { get; set; }
	
	public DateTimeOffset CreatedAt { get; set; }
	
	public float Size { get; set; }
}
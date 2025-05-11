namespace Model.Models;

public class File : ICreatedAt
{
	public string Name { get; set; } 
	
	public Guid Id { get; set; }
	
	public Guid UserId { get; set; }
	
	public User User { get; set; }
	
	public Guid FolderId { get; set; }
	
	public Folder Folder { get; set; }
	
	public DateTimeOffset UpdatedAt { get; set; }
	
	public DateTimeOffset CreatedAt { get; set; }
	
	public float Size { get; set; }
}
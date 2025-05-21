namespace Application.Models;

public class UserUploads
{
	public Guid UserId { get; set; }

	public string UserName { get; set; } = null!;

	public int FileCount { get; set; }

	public float TotalSize { get; set; }
}

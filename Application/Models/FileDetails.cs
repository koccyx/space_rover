namespace Application.Models;

public class FileDetails
{
	public Guid Id { get; set; }
	
	public string FileName { get; set; }
	
	public string OwnerName { get; set; }
	
	public string CompanyName { get; set; }
	
	public double Size { get; set; }
	
	public DateTime CreatedAt { get; set; }
}
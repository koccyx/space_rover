namespace Application.Models;
	
public class Company
{
	public Guid Id { get; set; }
	
	public string Name { get; set; }
	
	public float StorageLimit { get; set; }
	
	public float UsedStorage { get; set; }
}
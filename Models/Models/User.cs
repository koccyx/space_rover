namespace Model.Models;

public class User
{
	public Guid Id { get; set; }
	
	public string Name { get; set; }
	
	public string PasswordHash { get; set; }
	
	public ICollection<File> Files { get; set; }
	public Guid? CompanyId { get; set; }
	
	public Company? Company { get; set; }
}
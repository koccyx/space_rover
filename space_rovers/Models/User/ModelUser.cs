using System.Runtime.Serialization;
using FluentValidation;
using space_rovers.Models.Companies;

namespace space_rovers.Models.User;

[DataContract]
public class ModelUser
{
	[DataMember(Name = "Name")]
	public string Name { get; set; }
	
	[DataMember(Name = "Password")]
	public string Password { get; set; }
	
	[DataMember(Name = "CompanyId")]
	public Guid? CompanyId { get; set; }
	
	public sealed class Validator : AbstractValidator<ModelUser> 
	{
		public Validator() 
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.Password).NotEmpty(); 
		}
	}
}
using System.Runtime.Serialization;
using FluentValidation;
using space_rovers.Models.User;

namespace space_rovers.Models.Folder;

[DataContract]
public class ModelFolder
{
	[DataMember(Name = "Name")]
	public string Name { get; set; }
	
	[DataMember(Name = "CompanyId")]
	public Guid CompanyId { get; set; }
	
	public sealed class Validator : AbstractValidator<ModelFolder> 
	{
		public Validator() 
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.CompanyId).NotEmpty(); 
		}
	}
}
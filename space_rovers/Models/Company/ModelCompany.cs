using FluentValidation;

namespace space_rovers.Models.Companies;
using System.Runtime.Serialization;


[DataContract]
public class ModelCompany
{
	[DataMember(Name = "name")]
	public string Name { get; set; }
	
	[DataMember(Name = "storageLimit")]
	public float StorageLimit { get; set; }
	
	public sealed class Validator : AbstractValidator<ModelCompany>
	{
		public Validator()
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.StorageLimit).NotEmpty();
		}
	}
}
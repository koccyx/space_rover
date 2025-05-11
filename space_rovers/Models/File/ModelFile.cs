using System.Runtime.Serialization;
using FluentValidation;
using space_rovers.Models.Folder;

namespace space_rovers.Models.File;

[DataContract]
public class ModelFile
{
	[DataMember(Name = "Name")]
	public string Name { get; set; }
	
	[DataMember(Name = "UserId")]
	public Guid UserId { get; set; }
	
	[DataMember(Name = "CompanyId")]
	public Guid CompanyId { get; set; }
	
	[DataMember(Name = "Size")]
	public float Size { get; set; }
	
	[DataMember(Name = "FolderId")]
	public Guid FolderId { get; set; }
	
	public sealed class Validator : AbstractValidator<ModelFile> 
	{
		public Validator() 
		{
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.UserId).NotEmpty(); 
			RuleFor(x => x.CompanyId).NotEmpty(); 
			RuleFor(x => x.Size).NotEmpty(); 
			RuleFor(x => x.FolderId).NotEmpty(); 
		}
	}
}
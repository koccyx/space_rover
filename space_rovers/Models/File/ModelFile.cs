using System.Runtime.Serialization;
using FluentValidation;
using space_rovers.Models.Folder;

namespace space_rovers.Models.File;

[DataContract]
public class ModelFile
{
	[DataMember(Name = "FolderId")]
	public Guid FolderId { get; set; }
	
	public sealed class Validator : AbstractValidator<ModelFile> 
	{
		public Validator() 
		{
			RuleFor(x => x.FolderId).NotEmpty();
		}
	}
}
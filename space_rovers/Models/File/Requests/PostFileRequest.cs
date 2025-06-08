using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using space_rovers.Models.File;
using space_rovers.Models.File.Requests;

namespace space_rovers.Models.File.Requests;

/// <summary>
/// Создание дирректории
/// </summary>
public sealed record PostFileRequest
{
	[FromForm] 
	public required Guid FolderId { get; init; }
	
	[FromForm]
	public required IFormFile File { get; set; }	
	public sealed class Validator : AbstractValidator<PostFileRequest>
    	{
		    public Validator() 
		    {
			    RuleFor(x => x.FolderId).NotEmpty();
			    RuleFor(x => x.File).NotNull();
		    }
    	}
}
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
	[FromBody] 
	public required ModelFile File { get; init; }
	
	public sealed class Validator : AbstractValidator<PostFileRequest>
    	{
    		/// <inheritdoc />
    		public Validator() => RuleFor(x => x.File).NotNull().SetValidator(new ModelFile.Validator());
    	}
}
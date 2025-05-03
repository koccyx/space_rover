using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.Folder.Requests;

/// <summary>
/// Создание дирректории
/// </summary>
public sealed record PostFolderRequest
{
	[FromBody] 
	public required ModelFolder Folder { get; init; }
	
	public sealed class Validator : AbstractValidator<PostFolderRequest>
    	{
    		/// <inheritdoc />
    		public Validator() => RuleFor(x => x.Folder).NotNull().SetValidator(new ModelFolder.Validator());
    	}
}
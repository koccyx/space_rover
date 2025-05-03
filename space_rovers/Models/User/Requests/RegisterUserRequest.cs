using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.User.Requests;

/// <summary>
/// Создание пользователя
/// </summary>
public sealed record RegisterUserRequest
{
	[FromBody] 
	public required ModelUser User { get; init; }
	
	public sealed class Validator : AbstractValidator<RegisterUserRequest>
    	{
    		/// <inheritdoc />
    		public Validator() => RuleFor(x => x.User).NotNull().SetValidator(new ModelUser.Validator());
    	}
}

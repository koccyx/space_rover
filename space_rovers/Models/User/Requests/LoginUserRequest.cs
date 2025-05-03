using System.Runtime.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.User.Requests;

/// <summary>
/// Создание пользователя
/// </summary>
public sealed record LoginUserRequest
{
	[DataMember(Name = "login")] 
	public required string Login { get; init; }
	[DataMember(Name = "password")] 
	public required string Password { get; init; }
	
	public sealed class Validator : AbstractValidator<LoginUserRequest>
	{
		/// <inheritdoc />

		public Validator()
		{
			RuleFor(x => x.Login).NotNull();
			RuleFor(x => x.Password).NotNull();
		} 
	}
}

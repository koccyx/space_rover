using System.Runtime.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.User.Requests;

/// <summary>
/// Создание пользователя
/// </summary>
public sealed record LoginUserRequest
{
	[DataMember(Name = "name")] 
	public required string Name { get; init; }
	
	[DataMember(Name = "password")] 
	public required string Password { get; init; }
	
	public sealed class Validator : AbstractValidator<LoginUserRequest>
	{
		/// <inheritdoc />

		public Validator()
		{
			RuleFor(x => x.Name).NotNull();
			RuleFor(x => x.Password).NotNull();
		} 
	}
}

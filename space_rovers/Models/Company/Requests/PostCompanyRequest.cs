using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using space_rovers.Models.Companies;

namespace space_rovers.Models.Company.Requests;

/// <summary>
/// Создание компании
/// </summary>
public sealed record PostCompanyRequest
{
	[FromBody] 
	public required ModelCompany Company { get; init; }
	
	public sealed class Validator : AbstractValidator<PostCompanyRequest>
    	{
    		/// <inheritdoc />
    		public Validator() => RuleFor(x => x.Company).NotNull().SetValidator(new ModelCompany.Validator());
    	}
}
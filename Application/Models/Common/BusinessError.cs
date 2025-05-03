using Microsoft.AspNetCore.Mvc;

namespace Application.Models.Common;

public abstract class BusinessError
{
	protected abstract string Display { get; }
	
	protected abstract BusinessErrorHttpCodeCategory Category { get; }

	protected virtual ProblemDetails ToProblem() =>
		new()
		{
			Detail = Display,
			Status = Category switch
			{
				BusinessErrorHttpCodeCategory.RequestValidation => 400,
				BusinessErrorHttpCodeCategory.NotFound => 404,
				_ => 500
			}
		};
	
	public ObjectResult ToObjectResult() => new(ToProblem());
}
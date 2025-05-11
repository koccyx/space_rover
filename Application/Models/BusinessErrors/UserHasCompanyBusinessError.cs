using Application.Models.Common;

namespace Application.Models.BusinessErrors;

public class UserHasCompanyBusinessError : BusinessError
{
	public UserHasCompanyBusinessError() : base()
	{
	}

	/// <inheritdoc />
	protected override string Display => $"Текущий пользователь уже зарегистрирован в другой компании";

	/// <inheritdoc />
	protected override BusinessErrorHttpCodeCategory Category => BusinessErrorHttpCodeCategory.BusinessOperationInvalid;
}
using Application.Models.Common;

namespace Application.Models.BusinessErrors;

public class WrongPasswordBusinessError : BusinessError
{
	/// <summary>
	/// Имя пользователя
	/// </summary>
	public required string Name { get; init; }

	/// <inheritdoc />
	protected override string Display => $"Неверный пароль для {Name}";

	/// <inheritdoc />
	protected override BusinessErrorHttpCodeCategory Category => BusinessErrorHttpCodeCategory.BusinessOperationInvalid;
}
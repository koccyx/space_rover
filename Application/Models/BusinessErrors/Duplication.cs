using Application.Models.Common;

namespace Application.Models.BusinessErrors;

public sealed class UserDuplicationBusinessError : BusinessError
{
	/// <summary>
	/// Идентификатор компании
	/// </summary>
	public required Guid Id { get; init; }

	/// <summary>
	/// Название компании
	/// </summary>
	public required string Name { get; init; }

	/// <inheritdoc />
	protected override string Display => $"Пользователь с наименованием <${Name}> уже существует. Id={Id}";

	/// <inheritdoc />
	protected override BusinessErrorHttpCodeCategory Category => BusinessErrorHttpCodeCategory.BusinessOperationInvalid;
}
public sealed class CompanyDuplicationBusinessError : BusinessError
{
	/// <summary>
	/// Идентификатор компании
	/// </summary>
	public required Guid Id { get; init; }

	/// <summary>
	/// Название компании
	/// </summary>
	public required string Name { get; init; }

	/// <inheritdoc />
	protected override string Display => $"Компания с наименованием <${Name}> уже существует. Id={Id}";

	/// <inheritdoc />
	protected override BusinessErrorHttpCodeCategory Category => BusinessErrorHttpCodeCategory.BusinessOperationInvalid;
}
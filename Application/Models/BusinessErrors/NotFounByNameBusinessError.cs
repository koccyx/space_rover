using Application.Models.Common;

namespace Application.Models.BusinessErrors;

public class NotFounByNameBusinessError : BusinessError
{
	/// <summary>
	/// Идентификатор сущности
	/// </summary>
	public required string Name { get; init; }

	/// <summary>
	/// Название сущности
	/// </summary>
	public required string EntityName { get; init; }

	/// <inheritdoc />
	protected override string Display => $"{EntityName} с Name={Name} не существует";

	/// <inheritdoc />
	protected override BusinessErrorHttpCodeCategory Category => BusinessErrorHttpCodeCategory.NotFound;
}
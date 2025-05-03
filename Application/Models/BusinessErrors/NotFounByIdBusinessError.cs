using Application.Models.Common;

namespace Application.Models.BusinessErrors;

public class NotFounByIdBusinessError : BusinessError
{
	/// <summary>
	/// Идентификатор сущности
	/// </summary>
	public required Guid Id { get; init; }

	/// <summary>
	/// Название сущности
	/// </summary>
	public required string EntityName { get; init; }

	/// <inheritdoc />
	protected override string Display => $"{EntityName} с Id={Id} не существует";

	/// <inheritdoc />
	protected override BusinessErrorHttpCodeCategory Category => BusinessErrorHttpCodeCategory.NotFound;
}
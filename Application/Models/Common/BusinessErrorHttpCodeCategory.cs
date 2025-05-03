namespace Application.Models.Common;

public enum BusinessErrorHttpCodeCategory
{
	/// <summary>
	/// Некорректная операция
	/// </summary>
	BusinessOperationInvalid,

	/// <summary>
	/// Сущность не найдена
	/// </summary>
	NotFound,

	/// <summary>
	/// Некорректный запрос
	/// </summary>
	RequestValidation
}
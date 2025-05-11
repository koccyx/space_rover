using Application.Models.Common;

namespace Application.Models.BusinessErrors;

public class FileSizeError : BusinessError
{
	public float MaxFileSize { get; set; }
	
	public FileSizeError() : base()
	{
	}

	/// <inheritdoc />
	protected override string Display => $"Файл слишком большой, максимальная величина файла: {MaxFileSize}";

	/// <inheritdoc />
	protected override BusinessErrorHttpCodeCategory Category => BusinessErrorHttpCodeCategory.BusinessOperationInvalid;
}
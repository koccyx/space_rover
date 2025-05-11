using Application.Models.Common;
using Application.Models.Queries;
using Application.Models.Queries.Company;
using Application.Models.Queries.File;
using Application.Models.Queries.Folder;
using Application.Models.Queries.User;
using Application.Models.QueryResults;
using Application.Models.QueryResults.Company;
using Application.Models.QueryResults.File;
using Application.Models.QueryResults.Folder;
using Application.Models.QueryResults.User;
using OneOf;

namespace Application.Services;

public interface IFileService
{
	/// <summary>
	/// Получение списка файлов 
	/// </summary>
	public Task<GetFilesQueryResult> GetFiles(GetFilesQuery query, CancellationToken cancellationToken);

	/// <summary>
	/// Получение файла по Id  
	/// </summary>
	public Task<OneOf<GetFileQueryResult, BusinessError>> GetFileById(GetFileQuery query, CancellationToken cancellationToken);

	/// <summary>
	/// Создание файла 
	/// </summary>
	public Task<OneOf<PostFileQueryResult, BusinessError>> PostFile(PostFileQuery query, CancellationToken cancellationToken);
}
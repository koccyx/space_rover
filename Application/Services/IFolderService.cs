using Application.Models.Common;
using Application.Models.Queries;
using Application.Models.Queries.Company;
using Application.Models.Queries.Folder;
using Application.Models.Queries.User;
using Application.Models.QueryResults;
using Application.Models.QueryResults.Company;
using Application.Models.QueryResults.Folder;
using Application.Models.QueryResults.User;
using OneOf;

namespace Application.Services;

public interface IFolderService
{
	/// <summary>
	/// Получение списка дирректорий 
	/// </summary>
	public Task<GetFoldersQueryResult> GetFolders(GetFoldersQuery query, CancellationToken cancellationToken);

	/// <summary>
	/// Получение дирректории по Id  
	/// </summary>
	public Task<OneOf<GetFolderQueryResult, BusinessError>> GetFolderById(GetFolderQuery query, CancellationToken cancellationToken);

	/// <summary>
	/// Создание дирректории 
	/// </summary>
	public Task<OneOf<PostFolderQueryResult, BusinessError>> PostFolder(PostFolderQuery query, CancellationToken cancellationToken);
}
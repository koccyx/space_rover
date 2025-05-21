using Application.Models.Common;
using Application.Models.Queries.CompanyStorage;
using Application.Models.Queries.FileDetails;
using Application.Models.Queries.UploadsByDay;
using Application.Models.Queries.UserUploads;
using Application.Models.QueryResults.CompanyStorage;
using Application.Models.QueryResults.FileDetails;
using Application.Models.QueryResults.UploadsByDay;
using Application.Models.QueryResults.UserUploads;
using OneOf;

namespace Application.Services;

public interface IStatisticsService
{
	public Task<GetFileDetailsQueryResult> GetFileDetails(GetFileDetailsQuery query, CancellationToken token);

	public Task<GetUserUploadsQueryResult> GetUsersUploads(GetUserUploadsQuery query, CancellationToken token);

	public  Task<OneOf<GetCompanyStorageQueryResult, BusinessError>> GetCompanyStorageStatistics(GetCompanyStorageQuery query, CancellationToken token);

	public Task<GetUploadsByDayQueryResult> GetUploadsByDayStatistics(GetUploadsByDayQuery query, CancellationToken token);
}
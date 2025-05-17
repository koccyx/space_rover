using Application.Models;
using Application.Models.Common;
using Application.Models.Queries.FileDetails;
using Application.Models.QueryResults.FileDetails;
using OneOf;

namespace Application.Services;

public interface IStatisticsService
{
	public Task<GetFileDetailsQueryResult> GetFileDetails(GetFileDetailsQuery query, CancellationToken token);
}
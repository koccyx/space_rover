using Application.Models;

namespace space_rovers.Models.Statistics.Responses;

public class GetCompanyStorageStatisticsResponse
{
	public required CompanyStorageStatistics CompanyStorageStatistics { get; init; }
}
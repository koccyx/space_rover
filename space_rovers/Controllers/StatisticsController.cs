using Application.Models.Queries.FileDetails;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using space_rovers.Models.Folder.Responses;
using space_rovers.Models.Statistics.Requests;
using space_rovers.Models.Statistics.Responses;

namespace space_rovers.Controllers;

[ApiController]
[Route("/statistics")]
public class StatisticsController : ControllerBase
{
	private readonly IStatisticsService _statisticsService;
	
	public StatisticsController(IStatisticsService statisticsService)
	{
		_statisticsService = statisticsService ?? throw new ArgumentNullException(nameof(statisticsService));
	}

	[HttpGet("file-details")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFoldersResponse))]
	public async Task<ActionResult<GetFileDetailsStatisticsResponse>> GetFileDetailsStatistic(GetFileDetailsStatisticsRequest request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var getFileDetailsQuery = new GetFileDetailsQuery()
		{
			CompanyId = request.CompanyId
		};

		var response = await _statisticsService.GetFileDetails(getFileDetailsQuery, cancellationToken);

		return new GetFileDetailsStatisticsResponse()
		{
			FileDetails = response.FileDetails,
			TotalCount = response.TotalCount
		};
	}
}
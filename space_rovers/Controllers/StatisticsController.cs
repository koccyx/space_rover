using Application.Models.Queries.CompanyStorage;
using Application.Models.Queries.FileDetails;
using Application.Models.Queries.UploadsByDay;
using Application.Models.Queries.User;
using Application.Models.Queries.UserUploads;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using space_rovers.Models.Statistics.Requests;
using space_rovers.Models.Statistics.Responses;

namespace space_rovers.Controllers;

[ApiController]
[Route("/statistics")]
public class StatisticsController : ControllerBase
{
	private readonly IStatisticsService _statisticsService;
	private readonly IUserService _userService;

	public StatisticsController(IStatisticsService statisticsService, IUserService userService)
	{
		_statisticsService = statisticsService ?? throw new ArgumentNullException(nameof(statisticsService));
		_userService = userService ?? throw new ArgumentNullException(nameof(userService));
	}

	[HttpGet("file-details")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFileDetailsStatisticsResponse))]
	public async Task<ActionResult<GetFileDetailsStatisticsResponse>> GetFileDetailsStatistic(GetFileDetailsStatisticsRequest request,
		CancellationToken cancellationToken)
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

	[Authorize]
	[HttpGet("users-uploads")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersUploadsStatisticsResponse))]
	public async Task<ActionResult<GetUsersUploadsStatisticsResponse>> GetUsersUploadsStatistic([FromQuery]GetUsersUploadsStatisticsRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var userIdClaim = User.FindFirst("userId").Value;
		Guid.TryParse(userIdClaim, out var userId);

		var getUserCompanyQuery = new GetUserCompanyQuery()
		{
			UserId = userId
		};

		var userCompany = await _userService.GetUserCompany(getUserCompanyQuery, cancellationToken);
		
		if (userCompany.IsT1)
		{
			return userCompany.AsT1.ToObjectResult();
		}	

		var getUserUploadsQuery = new GetUserUploadsQuery()
		{
			CompanyId = userCompany.AsT0.Company.Id
		};

		var response = await _statisticsService.GetUsersUploads(getUserUploadsQuery, cancellationToken);

		return new GetUsersUploadsStatisticsResponse()
		{
			UserUploads = response.UserUploads,
			TotalCount = response.TotalCount
		};
	}
	
	
	[Authorize]
	[HttpGet("company-storage")]
	public async Task<ActionResult<GetCompanyStorageStatisticsResponse>> GetCompanyStorageStatistics([FromQuery] GetCompanyStorageStatisticRequest request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var userIdClaim = User.FindFirst("userId").Value;
		Guid.TryParse(userIdClaim, out var userId);

		var getUserCompanyQuery = new GetUserCompanyQuery()
		{
			UserId = userId
		};

		var userCompany = await _userService.GetUserCompany(getUserCompanyQuery, cancellationToken);
		
		if (userCompany.IsT1)
		{
			return userCompany.AsT1.ToObjectResult();
		}	

		var getCompanyStorageQuery = new GetCompanyStorageQuery()
		{
			CompanyId = userCompany.AsT0.Company.Id
		};

		var response = await _statisticsService.GetCompanyStorageStatistics(getCompanyStorageQuery, cancellationToken);

		if (response.IsT1)
		{
			return response.AsT1.ToObjectResult();
		}		
		
		return new GetCompanyStorageStatisticsResponse()
		{
			CompanyStorageStatistics = response.AsT0.CompanyStorageStatistics
		};
	}
	
	[Authorize]
	[HttpGet("uploads-by-day")]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUploadsByDayStatisticsResponse))]
	public async Task<ActionResult<GetUploadsByDayStatisticsResponse>> GetUploadsByDayStatistic([FromQuery]GetUploadsByDayStatisticRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var userIdClaim = User.FindFirst("userId").Value;
		Guid.TryParse(userIdClaim, out var userId);

		var getUserCompanyQuery = new GetUserCompanyQuery()
		{
			UserId = userId
		};

		var userCompany = await _userService.GetUserCompany(getUserCompanyQuery, cancellationToken);
		
		if (userCompany.IsT1)
		{
			return userCompany.AsT1.ToObjectResult();
		}	

		var uploadsQuery = new GetUploadsByDayQuery()
		{
			CompanyId = userCompany.AsT0.Company.Id
		};

		var response = await _statisticsService.GetUploadsByDayStatistics(uploadsQuery, cancellationToken);

		return new GetUploadsByDayStatisticsResponse()
		{
			UploadsByDayStatistics = response.UploadsByDay,
			TotalCount = response.TotalCount
		};
	}

}
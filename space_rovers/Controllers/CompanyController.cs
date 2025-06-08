using Application.Models.Queries.Company;
using Application.Models.Queries.User;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using space_rovers.Models.Company.Requests;
using space_rovers.Models.Company.Responses;
using space_rovers.Models.User.Responses;

namespace space_rovers.Controllers;

[ApiController]
[Route("/company")]
public class CompanyController : ControllerBase
{
	private readonly ICompanyService _companyService;
	private readonly IUserService _userService;

	public CompanyController(ICompanyService companyService, IUserService userService)
	{
		_companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
		_userService = userService ?? throw new ArgumentNullException(nameof(userService));
	}

	[Authorize]
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersResponse))]
	public async Task<ActionResult<GetCompaniesResponse>> GetCompanies(GetCompaniesRequest request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		var getCompaniesQuery = new GetCompaniesQuery() { };

		var response = await _companyService.GetCompanies(getCompaniesQuery, cancellationToken);

		return new GetCompaniesResponse()
		{
			Companies = response.Companies,
			TotalCount = response.TotalCount
		};
	}

	[Authorize]
	[HttpPost]
	public async Task<ActionResult<PostCompanyResponse>> PostCompany(PostCompanyRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		
		var userIdClaim = User.FindFirst("userId").Value;
		Guid.TryParse(userIdClaim, out var userId);

		var postCompanyQuery = new PostCompanyQuery
		{
			Name = request.Company.Name,
			StorageLimit = request.Company.StorageLimit,
			UsedStorage = 0,
			UserId = userId
		};

		var result = _companyService.PostCompany(postCompanyQuery, cancellationToken);

		if (result.Result.IsT1)
		{
			return result.Result.AsT1.ToObjectResult();
		}

		return Ok(new PostCompanyResponse()
		{
			Company = result.Result.AsT0.Company
		});
	}
	
	[Authorize]
	[HttpPatch("{userId::guid}/add-user")]
	public async Task<ActionResult<PatchUserToCompanyRequest>> AddUser(PatchUserToCompanyRequest request,
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

		var addUserToCompanyQuery = new AddUserToCompanyQuery()
		{
			UserId = request.UserId,
			CompanyId = userCompany.AsT0.Company.Id
		};

		var result = _companyService.AddUserToCompany(addUserToCompanyQuery, cancellationToken);

		if (result.Result.IsT1)
		{
			
			return result.Result.AsT1.ToObjectResult();
		}

		return Ok(new PatchUserToCompanyResponse()
		{
		});
	}	

	[HttpGet("{id::guid}")]
	public async Task<ActionResult<GetCompanyByIdResponse>> GetCompanyById(GetCompanyByIdRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var query = new GetCompanyQuery
		{
			Id = request.Id
		};

		var company = _companyService.GetCompanyById(query, cancellationToken);

		if (company.Result.IsT1)
		{
			return company.Result.AsT1.ToObjectResult();
		}

		return Ok(new GetCompanyByIdResponse()
		{
			Company = company.Result.AsT0.Company
		});
	}
}
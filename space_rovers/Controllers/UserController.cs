using Application.Models.Queries.User;
using Application.Models.QueryResults.User;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using space_rovers.Models.Companies.Requests.User;
using space_rovers.Models.User.Requests;
using space_rovers.Models.User.Responses;

namespace space_rovers.Controllers;

[ApiController]
[Route("/user")]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService ?? throw new ArgumentNullException(nameof(userService));
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetUsersResponse))]
	public async Task<ActionResult<GetUsersResponse>> GetUsers(GetUsersRequest request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var getCompaniesQuery = new GetUsersQuery() { };

		var response = await _userService.GetUsers(getCompaniesQuery, cancellationToken);

		return new GetUsersResponse()
		{
			Users = response.Users,
			TotalCount = response.TotalCount
		};
	}

	[HttpPost]
	[Route("login")]
	public async Task<ActionResult<LoginUserResponse>> LoginUser(LoginUserRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var loginUserQuery = new LoginUserQuery()
		{
			Name = request.Name,
			Password = request.Password
		};

		var result = _userService.LoginUser(loginUserQuery, cancellationToken);

		if (result.Result.IsT1)
		{
			return result.Result.AsT1.ToObjectResult();
		}

		Response.Cookies.Append("Authorization", $"Bearer {result.Result.AsT0.Token}");

		return Ok(new LoginUserResponse()
		{
			Token = result.Result.AsT0.Token
		});
	}
	
	[HttpPost]
	[Route("register")]
	public async Task<ActionResult<RegisterUserResponse>> RegisterUser(RegisterUserRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var postUserQuery = new RegisterUserQuery()
		{
			Name = request.User.Name,
			CompanyId = request.User.CompanyId,
			Password = request.User.Password
		};
		var result = _userService.RegisterUser(postUserQuery, cancellationToken);

		if (result.Result.IsT1)
		{
			return result.Result.AsT1.ToObjectResult();
		}

		var cookie = "Bearer " + result.Result.AsT0.Token;

		Response.Cookies.Append("Authorization", cookie); 
		
		return Ok(new RegisterUserResponse()
		{
			Token = result.Result.AsT0.Token
		});
	}

	[HttpGet("{id::guid}")]
	public async Task<ActionResult<GetUserByIdResponse>> GetUserById(GetUserByIdRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var query = new GetUserQuery()
		{
			Id = request.Id
		};

		var user = _userService.GetUserById(query, cancellationToken);

		if (user.Result.IsT1)
		{
			return user.Result.AsT1.ToObjectResult();
		}

		return Ok(new GetUserByIdResponse()
		{
			User = user.Result.AsT0.User
		});
	}
}
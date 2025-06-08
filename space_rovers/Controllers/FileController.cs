using Application.Models.Queries.File;
using Application.Models.Queries.User;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using space_rovers.Models.File.Requests;
using space_rovers.Models.File.Responses;
using space_rovers.Models.Folder.Requests;
using space_rovers.Models.Folder.Responses;

namespace space_rovers.Controllers;

[ApiController]
[Route("/file")]
public class FileController : ControllerBase
{
	private readonly IFileService _fileService;
	private readonly IUserService _userService;

	public FileController(IFileService fileService, IUserService userService)
	{
		_fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
		_userService = userService ?? throw new ArgumentNullException(nameof(userService));
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFilesResponse))]
	public async Task<ActionResult<GetFilesResponse>> GetFiles(GetFilesRequest request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var getFilesQuery = new GetFilesQuery()
		{
			FolderId = request.FolderId
		};

		var response = await _fileService.GetFiles(getFilesQuery, cancellationToken);

		return new GetFilesResponse()
		{
			Files = response.Files,
			TotalCount = response.TotalCount
		};
	}

	[Authorize]
	[HttpPost]
	public async Task<ActionResult<PostFileResponse>> PostFile(PostFileRequest request,
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

		Console.WriteLine(request.File.Length);

		var postFileQuery = new PostFileQuery()
		{
			File = request.File,
			Name = request.File.FileName,
			CompanyId = userCompany.AsT0.Company.Id,
			UserId = userId,
			FolderId = request.FolderId,
			Size = request.File.Length / 10000
		};

		var result = _fileService.PostFile(postFileQuery, cancellationToken);

		if (result.Result.IsT1)
		{
			return result.Result.AsT1.ToObjectResult();
		}

		return Ok(new PostFileResponse()
		{
			File = result.Result.AsT0.File
		});
	}

	[HttpGet("{id::guid}")]
	public async Task<ActionResult<GetFileByIdResponse>> GetFileById(GetFileByIdRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var query = new GetFileQuery()
		{
			Id = request.Id
		};

		var file = _fileService.GetFileById(query, cancellationToken);

		if (file.Result.IsT1)
		{
			return file.Result.AsT1.ToObjectResult();
		}

		return Ok(new GetFileByIdResponse()
		{
			File = file.Result.AsT0.File
		});
	}

	[HttpGet("pure/{id::guid}")]
	public async Task<IActionResult> GetFile(GetPureFileByIdRequest request, CancellationToken cancellationToken)
	{
		var file = await _fileService.GetPureFileById(new GetPureFileQuery() { Id = request.Id }, cancellationToken);

		if (file.IsT1)
		{
			return file.AsT1.ToObjectResult();
		}

		return File(file.AsT0.FileStream, file.AsT0.ContentType);
	}
}
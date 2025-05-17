using Application.Models.Queries.File;
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

	public FileController(IFileService fileService)
	{
		_fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
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
	public async Task<ActionResult<PostFileResponse>> PostFileById(PostFileRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var userIdClaim = User.FindFirst("userId").Value;
		
		Guid.TryParse(userIdClaim, out var userId);
		
		var postFileQuery = new PostFileQuery()
		{
			Name = request.File.Name,
			CompanyId = request.File.CompanyId,
			UserId = userId,
			FolderId = request.File.FolderId,
			Size = request.File.Size
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
	public async Task<ActionResult<GetFileByIdResponse>> GetFileById(GetFolderByIdRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var query = new GetFileQuery()
		{
			Id = request.Id
		};

		var user = _fileService.GetFileById(query, cancellationToken);

		if (user.Result.IsT1)
		{
			return user.Result.AsT1.ToObjectResult();
		}

		return Ok(new GetFileByIdResponse()
		{
			File = user.Result.AsT0.File
		});
	}
}
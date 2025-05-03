using Application.Models.Queries.Folder;
using Application.Models.Queries.User;
using Application.Models.QueryResults.Folder;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using space_rovers.Models.Companies.Requests.User;
using space_rovers.Models.Folder.Requests;
using space_rovers.Models.Folder.Responses;
using space_rovers.Models.User.Requests;
using space_rovers.Models.User.Responses;

namespace space_rovers.Controllers;

[ApiController]
[Route("/folder")]
public class FolderController : ControllerBase
{
	private readonly IFolderService _folderService;

	public FolderController(IFolderService folderService)
	{
		_folderService = folderService ?? throw new ArgumentNullException(nameof(folderService));
	}

	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFoldersResponse))]
	public async Task<ActionResult<GetFoldersResponse>> GetFolders(GetFoldersRequest request, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var getFoldersQuery = new GetFoldersQuery()
		{
			CompanyId = request.CompanyId
		};

		var response = await _folderService.GetFolders(getFoldersQuery, cancellationToken);

		return new GetFoldersResponse()
		{
			Folders = response.Folders,
			TotalCount = response.TotalCount
		};
	}

	[HttpPost]
	public async Task<ActionResult<PostFolderResponse>> PostFolderByIdResponse(PostFolderRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);
		
		var postFolderQuery = new PostFolderQuery()
		{
			Name = request.Folder.Name,
			CompanyId = request.Folder.CompanyId,
			UserId = request.Folder.UserId
		};
		var result = _folderService.PostFolder(postFolderQuery, cancellationToken);

		if (result.Result.IsT1)
		{
			return result.Result.AsT1.ToObjectResult();
		}

		return Ok(new PostFolderResponse()
		{
			Folder = result.Result.AsT0.Folder
		});
	}

	[HttpGet("{id::guid}")]
	public async Task<ActionResult<GetFolderByIdResponse>> GetFolderByIdResponse(GetFolderByIdRequest request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		var query = new GetFolderQuery()
		{
			Id = request.Id
		};

		var user = _folderService.GetFolderById(query, cancellationToken);

		if (user.Result.IsT1)
		{
			return user.Result.AsT1.ToObjectResult();
		}

		return Ok(new GetFolderByIdResponse()
		{
			Folder =  user.Result.AsT0.Folder
		});
	}
}
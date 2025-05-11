using Application.Models.Common;
using Application.Models.BusinessErrors;
using Application.Models.Queries.Folder;
using Application.Models.Queries.User;
using Application.Models.QueryResults.Folder;
using AutoMapper;
using Infrastracture;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Application.Services.impl;

public sealed class FolderService : IFolderService
{
	private readonly ApplicationDbContext _db;
	public readonly IMapper _mapper;

	public FolderService(ApplicationDbContext db, IMapper mapper)
	{
		_db = db ?? throw new ArgumentException(nameof(db));
		_mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
	}

	public async Task<OneOf<GetFolderQueryResult, BusinessError>> GetFolderById(GetFolderQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var folder = await _db.Folders.SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

		if (folder is null)
		{
			return new NotFounByIdBusinessError()
			{
				Id = query.Id,
				EntityName = nameof(Application.Models.Folder)
			};
		}

		var mappedFolder = _mapper.Map<Application.Models.Folder>(folder);

		return new GetFolderQueryResult()
		{
			Folder = mappedFolder
		};
	}

	public async Task<GetFoldersQueryResult> GetFolders(GetFoldersQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var companyQuery = _db.Folders.AsQueryable();

		var folders = companyQuery
			.Where(x => x.CompanyId == query.CompanyId);
		
		var totalCount = await folders.CountAsync(cancellationToken);

		if (totalCount == 0)
		{
			return new GetFoldersQueryResult()
			{
				Folders = [],
				TotalCount = 0
			};
		}

		var foldersList = await companyQuery.ToListAsync();
		
		var mappedFolders = _mapper.Map<Application.Models.Folder[]>(foldersList);

		return new GetFoldersQueryResult()
		{
			Folders = mappedFolders,
			TotalCount = totalCount
		};
	}

	public async Task<OneOf<PostFolderQueryResult, BusinessError>> PostFolder(PostFolderQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var existingFolder =
			await _db.Folders.SingleOrDefaultAsync(x => x.Name == query.Name && x.CompanyId == query.CompanyId, cancellationToken);

		if (existingFolder != null)
		{
			return new CompanyDuplicationBusinessError()
			{
				Id = existingFolder.Id,
				Name = existingFolder.Name
			};
		}

		if (!await _db.Companies.Where(x => x.Id == query.CompanyId).AnyAsync(cancellationToken))
		{
			return new NotFounByIdBusinessError()
			{
				Id = query.CompanyId,
				EntityName = nameof(Application.Models.Company)
			};
		}

		var folder = new Model.Models.Folder()
		{
			Name = query.Name,
			CompanyId = query.CompanyId,
			UserId = query.UserId,
		};

		await _db.Folders.AddAsync(folder, cancellationToken);
		await _db.SaveChangesAsync(cancellationToken);

		var response = await GetFolderById(new GetFolderQuery() { Id = folder.Id }, cancellationToken);
		if (response.IsT0)
		{
			return new PostFolderQueryResult()
			{
				Folder = response.AsT0.Folder
			};
		}

		return response.AsT1;
	}
}
using Application.Models.Common;
using Application.Models.BusinessErrors;
using Application.Models.Queries.File;
using Application.Models.QueryResults.File;
using AutoMapper;
using Infrastracture;
using Infrastracture.S3;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using OneOf;

namespace Application.Services.impl;

public sealed class FileService : IFileService
{
	private readonly ApplicationDbContext _db;
	private readonly S3Repository _s3;
	public readonly IMapper _mapper;

	public FileService(ApplicationDbContext db, IMapper mapper, S3Repository s3)
	{
		_db = db ?? throw new ArgumentException(nameof(db));
		_mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
		_s3 = s3 ?? throw new ArgumentException(nameof(s3));
	}

	public async Task<OneOf<GetPureFileQueryResult, BusinessError>> GetPureFileById(GetPureFileQuery query,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var exists = await _db.Files.AnyAsync(x => x.Id == query.Id, cancellationToken);

		if (!exists)
		{
			return new NotFounByIdBusinessError()
			{
				Id = query.Id,
				EntityName = nameof(Application.Models.File)
			};
		}

		try
		{
			var file = await _s3.GetFile(query.Id);

			return new GetPureFileQueryResult()
			{
				FileStream = file.FileStream,
				ContentType = file.ContentType
			};
		}
		catch (Exception e)
		{
			Console.WriteLine(e);

			return new NotFounByIdBusinessError()
			{
				Id = query.Id,
				EntityName = nameof(Application.Models.File)
			};
		}
	}

	public async Task<OneOf<GetFileQueryResult, BusinessError>> GetFileById(GetFileQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var file = await _db.Files.SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

		if (file is null)
		{
			return new NotFounByIdBusinessError()
			{
				Id = query.Id,
				EntityName = nameof(Application.Models.File)
			};
		}

		var mappedFile = _mapper.Map<Application.Models.File>(file);

		return new GetFileQueryResult()
		{
			File = mappedFile
		};
	}

	public async Task<GetFilesQueryResult> GetFiles(GetFilesQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var fileQuery = _db.Files.AsQueryable();

		var files = fileQuery
			.Where(x => x.FolderId == query.FolderId);

		var totalCount = await files.CountAsync(cancellationToken);

		if (totalCount == 0)
		{
			return new GetFilesQueryResult()
			{
				Files = [],
				TotalCount = 0
			};
		}

		var filesList = await files.ToListAsync();

		var mappedFiles = _mapper.Map<Application.Models.File[]>(filesList);

		return new GetFilesQueryResult()
		{
			Files = mappedFiles,
			TotalCount = totalCount
		};
	}

	public async Task<OneOf<PostFileQueryResult, BusinessError>> PostFile(PostFileQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var existingFile =
			await _db.Files.SingleOrDefaultAsync(x => x.Name == query.Name && x.FolderId == query.FolderId, cancellationToken);

		if (existingFile is not null)
		{
			return new CompanyDuplicationBusinessError()
			{
				Id = existingFile.Id,
				Name = existingFile.Name
			};
		}

		var file = new Model.Models.File()
		{
			Id = Guid.NewGuid(),
			Name = query.Name,
			UserId = query.UserId,
			FolderId = query.FolderId,
			Size = query.Size
		};

		var company = await _db.Folders
			.Where(f => f.Id == file.FolderId)
			.Select(f => f.Company)
			.SingleOrDefaultAsync(cancellationToken);

		if (company is null)
		{
			return new NotFounByIdBusinessError()
			{
				EntityName = nameof(Application.Models.Company),
				Id = Guid.Empty
			};
		}

		// if (company.UsedStorage + file.Size > company.StorageLimit)
		// {
		// 	return new FileSizeError()
		// 	{
		// 		MaxFileSize = company.StorageLimit - company.UsedStorage
		// 	};
		// }
		//
		// company.UsedStorage += file.Size;


		try
		{
			await _db.Files.AddAsync(file, cancellationToken);
			await _s3.AddImage(query.File, file.Id);
			await _db.SaveChangesAsync(cancellationToken);
		}
		catch (DbUpdateException dbEx) when (dbEx.InnerException is PostgresException pgEx)
		{
			Console.WriteLine("LIMIT");
			return new FileSizeError()
			{
				MaxFileSize = company.StorageLimit - company.UsedStorage
			};

			throw;
		}

		var response = await GetFileById(new GetFileQuery() { Id = file.Id }, cancellationToken);
		if (response.IsT0)
		{
			return new PostFileQueryResult()
			{
				File = response.AsT0.File
			};
		}

		return response.AsT1;
	}
}
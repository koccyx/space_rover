using Application.Models.Common;
using Application.Models.Queries.CompanyStorage;
using Application.Models.Queries.FileDetails;
using Application.Models.Queries.UploadsByDay;
using Application.Models.Queries.UserUploads;
using Application.Models.QueryResults.CompanyStorage;
using Application.Models.QueryResults.FileDetails;
using Application.Models.QueryResults.UploadsByDay;
using Application.Models.QueryResults.UserUploads;
using AutoMapper;
using Infrastracture;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using OneOf;


namespace Application.Services.impl;

public sealed class StatisticService : IStatisticsService
{
	private readonly ApplicationDbContext _db;
	public readonly IMapper _mapper;

	public StatisticService(ApplicationDbContext db, IMapper mapper)
	{
		_db = db ?? throw new ArgumentException(nameof(db));
		_mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
	}

	public async Task<GetFileDetailsQueryResult> GetFileDetails(GetFileDetailsQuery query, CancellationToken token)
	{
		ArgumentNullException.ThrowIfNull(query);

		var fileQuery = _db.FileDetailsViews.AsQueryable();
		var files = fileQuery
			.Join(
				_db.Companies,
				fdv => fdv.CompanyName,
				c => c.Name,
				(fdv, c) => new { fdv, c })
			.Where(x => x.c.Id == query.CompanyId)
			.Select(x => x.fdv);

		var totalCount = await files.CountAsync(token);

		if (totalCount == 0)
		{
			return new GetFileDetailsQueryResult()
			{
				FileDetails = [],
				TotalCount = 0
			};
		}

		var filesList = await files.ToListAsync();

		var mappedFiles = _mapper.Map<Application.Models.FileDetails[]>(filesList);

		return new GetFileDetailsQueryResult()
		{
			FileDetails = mappedFiles,
			TotalCount = totalCount
		};
	}

	public async Task<GetUserUploadsQueryResult> GetUsersUploads(GetUserUploadsQuery query, CancellationToken token)
	{
		ArgumentNullException.ThrowIfNull(query);

		var fileQuery = _db.UserFileViews.AsQueryable();

		var companyUsers = _db.Users.Where(u => u.CompanyId == query.CompanyId)
			.Select(u => u.Id);

		var usersUploads = fileQuery
			.Where(uf => companyUsers.Contains(uf.UserId));

		var totalCount = await usersUploads.CountAsync(token);

		if (totalCount == 0)
		{
			return new GetUserUploadsQueryResult()
			{
				UserUploads = [],
				TotalCount = 0
			};
		}

		var userUploadsList = await usersUploads.ToListAsync();

		var mappedFiles = _mapper.Map<Application.Models.UserUploads[]>(userUploadsList);

		return new GetUserUploadsQueryResult()
		{
			UserUploads = mappedFiles,
			TotalCount = totalCount
		};
	}

	public async Task<OneOf<GetCompanyStorageQueryResult, BusinessError>> GetCompanyStorageStatistics(GetCompanyStorageQuery query,
		CancellationToken token)
	{
		ArgumentNullException.ThrowIfNull(query);

		var stats = await _db
			.Set<UsedStorageStatistics>()
			.FromSqlInterpolated(
				$"SELECT m.percents AS \"Percents\", m.usedstorage AS \"UsedStorage\", m.storagelimit AS \"StorageLimit\"  FROM get_percents_of_used_storage({query.CompanyId}) m")
			.SingleOrDefaultAsync(token);

		var mappedStatistics = _mapper.Map<Application.Models.CompanyStorageStatistics>(stats);

		return new GetCompanyStorageQueryResult()
		{
			CompanyStorageStatistics = mappedStatistics
		};
	}

	public async Task<GetUploadsByDayQueryResult> GetUploadsByDayStatistics(GetUploadsByDayQuery query, CancellationToken token)
	{
		ArgumentNullException.ThrowIfNull(query);
		
		var stats = _db
			.Set<UploadsByDayStatistics>()
			.FromSqlInterpolated(
				$"SELECT m.uploadday AS \"UploadDate\", m.count AS \"Uploads\"  FROM get_company_days_statistic({query.CompanyId}) m");

		var totalCount = await stats.CountAsync(token);

		if (totalCount == 0)
		{
			return new GetUploadsByDayQueryResult()
			{
				UploadsByDay = [],
				TotalCount = 0
			};
		}

		var uploadsList = await stats.ToListAsync();

		var mappedUploads = _mapper.Map<Application.Models.UploadsByDayStatistics[]>(uploadsList);

		return new GetUploadsByDayQueryResult()
		{
			UploadsByDay = mappedUploads,
			TotalCount = totalCount
		};
	}
}
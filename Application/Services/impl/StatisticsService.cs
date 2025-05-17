using Application.Models.Queries.FileDetails;
using Application.Models.QueryResults.FileDetails;
using AutoMapper;
using Infrastracture;
using Microsoft.EntityFrameworkCore;

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
}
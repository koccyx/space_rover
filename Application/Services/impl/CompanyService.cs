using Application.Models.Common;
using Application.Models.BusinessErrors;
using Application.Models.Queries;
using Application.Models.Queries.Company;
using Application.Models.QueryResults.Company;
using AutoMapper;
using Infrastracture;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Application.Services.impl;

public sealed class CompanyService : ICompanyService
{
	private readonly ApplicationDbContext _db;
	public readonly IMapper _mapper;

	public CompanyService(ApplicationDbContext db, IMapper mapper)
	{
		_db = db ?? throw new ArgumentException(nameof(db));
		_mapper = mapper ?? throw new ArgumentException(nameof(_mapper));
	}

	public async Task<OneOf<GetCompanyQueryResult, BusinessError>> GetCompanyById(GetCompanyQuery query,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var company = await _db.Companies.SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

		if (company is null)
		{
			return new NotFounByIdBusinessError()
			{
				Id = query.Id,
				EntityName = nameof(Application.Models.Company)
			};
		}

		var mappedCompany = _mapper.Map<Application.Models.Company>(company);

		return new GetCompanyQueryResult()
		{
			Company = mappedCompany
		};
	}
	
	public async Task<OneOf<GetCompanyQueryResult, BusinessError>> GetCompanyByName(GetCompanyByNameQuery query,
    		CancellationToken cancellationToken)
    	{
    		ArgumentNullException.ThrowIfNull(query);
    
    		var company = await _db.Companies.SingleOrDefaultAsync(x => x.Name == query.Name, cancellationToken);
    
    		if (company != null)
    		{
    			return new NotFounByNameBusinessError()
    			{
    				Name = query.Name,
    				EntityName = nameof(Application.Models.Company)
    			};
    		}
    
    		var mappedCompany = _mapper.Map<Application.Models.Company>(company);
    
    		return new GetCompanyQueryResult()
    		{
    			Company = mappedCompany
    		};
    	}

	public async Task<GetCompaniesQueryResult> GetCompanies(GetCompaniesQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var companiesQuery = _db.Companies.AsQueryable();

		var totalCount = await companiesQuery.CountAsync(cancellationToken);

		if (totalCount == 0)
		{
			return new GetCompaniesQueryResult()
			{
				Companies = [],
				TotalCount = 0
			};
		}

		var companies = companiesQuery.ToArray();
		var mappedCompanies = _mapper.Map<Application.Models.Company[]>(companies);
		
		return new GetCompaniesQueryResult()
		{
			Companies = mappedCompanies,
			TotalCount = totalCount
		};
	}

	public async Task<OneOf<PostCompanyQueryResult, BusinessError>> PostCompany(PostCompanyQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var existingCompany = await _db.Companies.SingleOrDefaultAsync(x => x.Name == query.Name, cancellationToken);
		
		if (existingCompany != null)
		{
			return new CompanyDuplicationBusinessError()
			{
				Id = existingCompany.Id,
				Name = existingCompany.Name
			};
		}

		var user = await _db.Users.Where(x => x.Id == query.UserId)
			.SingleAsync(cancellationToken);
		
		if (user.CompanyId is not null)
		{
			return new UserHasCompanyBusinessError();
		}

		var company = new Model.Models.Company()
		{
			Name = query.Name,
			StorageLimit = query.StorageLimit,
			UsedStorage = query.UsedStorage
		};

		await _db.Companies.AddAsync(company, cancellationToken);
		user.CompanyId = company.Id;
		
		await _db.SaveChangesAsync(cancellationToken);
		
		var response = await GetCompanyById(new GetCompanyQuery {Id = company.Id}, cancellationToken);
		if (response.IsT0)
		{
			return new PostCompanyQueryResult()
			{
				Company = response.AsT0.Company
			};
		}
	
		return response.AsT1;
	}
	
	public async Task<OneOf<AddUserToCompanyQueryResult, BusinessError>> AddUserToCompany(AddUserToCompanyQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);
		
		if (!await _db.Companies.AnyAsync(x => x.Id == query.CompanyId, cancellationToken))
		{
			return new NotFounByIdBusinessError()
			{
				Id = query.CompanyId,
				EntityName	= nameof(Application.Models.Company) 
			};
		}

		var user = await _db.Users.Where(x => x.Id == query.UserId)
			.SingleOrDefaultAsync(cancellationToken);
		
		if (user is null)
		{
			return new NotFounByIdBusinessError()
			{
				Id = query.UserId,
				EntityName	= nameof(Application.Models.User) 
			};
		}

		user.CompanyId = query.CompanyId;
		
		await _db.SaveChangesAsync(cancellationToken);
		
		return new AddUserToCompanyQueryResult()
		{
		};
	}	
}
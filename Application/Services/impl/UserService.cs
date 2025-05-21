using Application.Interfaces;
using Application.Models;
using Application.Models.Common;
using Application.Models.BusinessErrors;
using Application.Models.Queries;
using Application.Models.Queries.User;
using Application.Models.QueryResults.User;
using Application.utils.jwt;
using AutoMapper;
using Infrastracture;
using Infrastracture.Utils;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Application.Services.impl;

public sealed class UserService : IUserService
{
	private readonly ApplicationDbContext _db;
	public readonly IMapper _mapper;
	public readonly IPasswordHasher _hasher;
	public readonly IJwtProvider _jwtProvider;

	public UserService(ApplicationDbContext db, IMapper mapper, IPasswordHasher hasher, IJwtProvider jwtProvider)
	{
		_db = db ?? throw new ArgumentException(nameof(db));
		_mapper = mapper ?? throw new ArgumentException(nameof(mapper));
		_hasher = hasher ?? throw new ArgumentException(nameof(hasher));
		_jwtProvider = jwtProvider ?? throw new ArgumentException(nameof(jwtProvider));
	}

	public async Task<OneOf<GetUserQueryResult, BusinessError>> GetUserById(GetUserQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == query.Id, cancellationToken);

		if (user is null)
		{
			return new NotFounByIdBusinessError()
			{
				Id = query.Id,
				EntityName = nameof(Application.Models.User)
			};
		}

		var mappedUser = _mapper.Map<Application.Models.User>(user);

		return new GetUserQueryResult()
		{
			User = mappedUser
		};
	}

	public async Task<GetUsersQueryResult> GetUsers(GetUsersQuery query, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var usersQuery = _db.Users.AsQueryable();

		var totalCount = await usersQuery.CountAsync(cancellationToken);

		if (totalCount == 0)
		{
			return new GetUsersQueryResult()
			{
				Users = [],
				TotalCount = 0
			};
		}

		var users = usersQuery.ToArray();
		var mappedUsers = _mapper.Map<Application.Models.User[]>(users);

		return new GetUsersQueryResult()
		{
			Users = mappedUsers,
			TotalCount = totalCount
		};
	}

	public async Task<OneOf<RegisterUserQueryResult, BusinessError>> RegisterUser(RegisterUserQuery query,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var existingUser =
			await _db.Users.SingleOrDefaultAsync(x => x.Name == query.Name && x.CompanyId == query.CompanyId, cancellationToken);

		if (existingUser != null)
		{
			return new UserDuplicationBusinessError()
			{
				Id = existingUser.Id,
				Name = existingUser.Name
			};
		}

		var user = new Model.Models.User()
		{
			Name = query.Name,
			PasswordHash = _hasher.Generate(query.Password),
			CompanyId = query.CompanyId,
		};

		await _db.Users.AddAsync(user, cancellationToken);
		await _db.SaveChangesAsync(cancellationToken);

		var savedUser = await GetUserById(new GetUserQuery() { Id = user.Id, CompanyId = user.CompanyId.GetValueOrDefault() },
			cancellationToken);

		var result = _hasher.Verify(query.Password, savedUser.AsT0.User.PasswordHash);

		if (!result)
		{
			return new WrongPasswordBusinessError
			{
				Name = savedUser.AsT0.User.Name
			};
		}

		var token = _jwtProvider.GenerateToken(savedUser.AsT0.User);

		return new RegisterUserQueryResult()
		{
		Token = token	
		};
	}

	public async Task<OneOf<GetUserCompanyQueryResult, BusinessError>> GetUserCompany(GetUserCompanyQuery query,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(query);

		var usersCompanies =  _db.Companies.Join(_db.Users,
			c => c.Id,
			u => u.CompanyId,
			(c, u) => new { c, u });

		var company = await usersCompanies.Where(cu => cu.u.Id == query.UserId).Select(cu => cu.c).SingleOrDefaultAsync(cancellationToken);

		if (company is null)
		{
			return new NotFounByIdBusinessError()
			{
				Id = query.UserId,
				EntityName = nameof(Application.Models.Company)
			};
		}

		var mappedCompany = _mapper.Map<Application.Models.Company>(company);

		return new GetUserCompanyQueryResult()
		{
			Company = mappedCompany
		};	
	}
	
	public async Task<OneOf<LoginUserQueryResult, BusinessError>> LoginUser(LoginUserQuery query, CancellationToken cancellationToken)
	{
		var existingUser = await _db.Users.SingleOrDefaultAsync(x => x.Name == query.Name);

		if (existingUser is null)
		{
			return new NotFounByNameBusinessError
			{
				Name = query.Name,
				EntityName = nameof(Application.Models.User)
			};
		}

		var result = _hasher.Verify(query.Password, existingUser.PasswordHash);

		if (!result)
		{
			return new WrongPasswordBusinessError
			{
				Name = existingUser.Name
			};
		}

		var user = _mapper.Map<Application.Models.User>(existingUser);

		var token = _jwtProvider.GenerateToken(user);

		return new LoginUserQueryResult() { Token = token };
	}
}
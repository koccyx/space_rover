using Application.Models.Common;
using Application.Models.Queries;
using Application.Models.Queries.Company;
using Application.Models.Queries.User;
using Application.Models.QueryResults;
using Application.Models.QueryResults.Company;
using Application.Models.QueryResults.User;
using OneOf;

namespace Application.Services;

public interface IUserService
{
	/// <summary>
	/// Получение списка пользователей 
	/// </summary>
	public Task<GetUsersQueryResult> GetUsers(GetUsersQuery query, CancellationToken cancellationToken);

	/// <summary>
	/// Получение пользователя по Id  
	/// </summary>
	public Task<OneOf<GetUserQueryResult, BusinessError>> GetUserById(GetUserQuery query, CancellationToken cancellationToken);

	/// <summary>
	/// Регистрация пользователя 
	/// </summary>
	public Task<OneOf<RegisterUserQueryResult, BusinessError>> RegisterUser(RegisterUserQuery query, CancellationToken cancellationToken);
	
	/// <summary>
	/// Логин пользователя 
	/// </summary>
	public Task<OneOf<LoginUserQueryResult, BusinessError>> LoginUser(LoginUserQuery query, CancellationToken cancellationToken);
}

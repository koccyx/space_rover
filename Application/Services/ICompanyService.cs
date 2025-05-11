using Application.Models.Common;
using Application.Models.Queries;
using Application.Models.Queries.Company;
using Application.Models.QueryResults;
using Application.Models.QueryResults.Company;
using OneOf;

namespace Application.Services;

public interface ICompanyService
{
	/// <summary>
	/// Получение списка компаний 
	/// </summary>
	public Task<GetCompaniesQueryResult> GetCompanies(GetCompaniesQuery query, CancellationToken cancellationToken);

	/// <summary>
	/// Получение компании по Id  
	/// </summary>
	public Task<OneOf<GetCompanyQueryResult, BusinessError>> GetCompanyById(GetCompanyQuery query, CancellationToken cancellationToken);

	/// <summary>
	/// Добавление пользователя в компанию  
	/// </summary>
	public Task<OneOf<AddUserToCompanyQueryResult, BusinessError>> AddUserToCompany(AddUserToCompanyQuery query, CancellationToken cancellationToken);

	/// <summary>
	/// Создание компании  
	/// </summary>
	public Task<OneOf<PostCompanyQueryResult, BusinessError>> PostCompany(PostCompanyQuery query, CancellationToken cancellationToken);

	/// <summary>
	/// Получение компании по имени 
	/// </summary>
	public Task<OneOf<GetCompanyQueryResult, BusinessError>> GetCompanyByName(GetCompanyByNameQuery query,
		CancellationToken cancellationToken);
}
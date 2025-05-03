using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Application.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.utils.jwt;

public sealed class JwtProvider : IJwtProvider
{
	private readonly JwtOptions _options;

	public JwtProvider(IOptions<JwtOptions> options)
	{
		_options = options.Value;
	}
	public string GenerateToken(User user)
	{
		Claim[] claims = [
			new("userId", user.Id.ToString()),
			new("name", user.Name)
		];
		
		var signingCridentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			signingCredentials: signingCridentials,
			claims: claims,
			expires: DateTime.UtcNow.AddHours(_options.Expires)
		);

		var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

		return tokenValue;
	}
}
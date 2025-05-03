namespace Application.utils.jwt;

public class JwtOptions
{
	public string SecretKey { get; init; } = String.Empty;
	
	public int Expires { get; init; }
}
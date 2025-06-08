namespace Application.Models.QueryResults.File;

public record GetPureFileQueryResult
{
	public Stream FileStream { get; set; }
	
	public string ContentType { get; set; }
}
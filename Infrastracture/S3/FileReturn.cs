using System.Net.Mime;

namespace Infrastracture.S3;

public record FileReturn
{
	public Stream FileStream { get; set; }
	
	public string ContentType { get; set; }
}
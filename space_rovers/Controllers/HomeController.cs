using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace space_rovers.Controllers;

[ApiController]
[Route("/s3")]
public class HomeController : Controller
{
	private readonly IAmazonS3 _s3Client;
	private readonly S3Settings _s3Settings;

	public HomeController(IAmazonS3 s3Client, IOptions<S3Settings> s3Settings)
	{
		_s3Client = s3Client;
		_s3Settings = s3Settings.Value;
	}

	[HttpPost]
	public async Task<IActionResult> Greeting(IFormFile file)
	{
		if (file.Length == 0)
		{
			return BadRequest("No file");
		}

		using var stream = file.OpenReadStream();

		var key = Guid.NewGuid();

		var putReq = new PutObjectRequest()
		{
			BucketName = _s3Settings.BucketName,
			Key = $"images/{key}",
			InputStream = stream,
			ContentType = file.ContentType,
			Metadata =
			{
				["filename"] = file.FileName
			}
		};
		
		await _s3Client.PutObjectAsync(putReq);

		return Ok(key);
	}

	[HttpGet]
	public async Task<IActionResult> GetFile(string key)
	{
		var getReq = new GetObjectRequest
		{
			BucketName = _s3Settings.BucketName,
			Key = $"images/{key}"
		};

		var resp = await _s3Client.GetObjectAsync(getReq);

		// return mem
		return File(resp.ResponseStream, resp.Headers.ContentType);
	}
}
using System.Net;
using System.Net.Mime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using space_rovers;


namespace Infrastracture.S3;

public class S3Repository
{
	private readonly IAmazonS3 _s3Client;
	private readonly S3Settings _s3Settings;

	public S3Repository(IAmazonS3 s3Client, IOptions<S3Settings> s3Settings)
	{
		_s3Client = s3Client;
		_s3Settings = s3Settings.Value;
	}

	public async Task AddImage(IFormFile file, Guid id)
	{
		if (file.Length == 0)
		{
			throw new Exception("No file");
		}

		using var stream = file.OpenReadStream();

		var putReq = new PutObjectRequest()
		{
			BucketName = _s3Settings.BucketName,
			Key = $"images/{id}",
			InputStream = stream,
			ContentType = file.ContentType,
			Metadata =
			{
				["filename"] = file.FileName
			}
		};

		await _s3Client.PutObjectAsync(putReq);
	}
	
	public async Task<FileReturn> GetFile(Guid key)
	{
		var getReq = new GetObjectRequest
		{
			BucketName = _s3Settings.BucketName,
			Key = $"images/{key}"
		};

		var resp = await _s3Client.GetObjectAsync(getReq);

		// return mem
		return new FileReturn()
		{
			FileStream = resp.ResponseStream,
			ContentType = resp.Headers.ContentType
		};
	}
}
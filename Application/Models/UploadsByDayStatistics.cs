namespace Application.Models;

public class UploadsByDayStatistics
{
	public DateTimeOffset UploadDate { get; init; }
	
	public int Uploads { get; init; }
}
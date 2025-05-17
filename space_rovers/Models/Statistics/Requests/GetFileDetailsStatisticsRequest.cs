using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace space_rovers.Models.Statistics.Requests;

public sealed record GetFileDetailsStatisticsRequest
{
	[FromQuery(Name = "companyId")]
	[Required]
	public required Guid CompanyId { get; set; }
}
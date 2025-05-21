using AutoMapper;
using Model.Models;
using File = Model.Models.File;

namespace space_rovers.MappingProfile;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Company, Application.Models.Company>();
		CreateMap<User, Application.Models.User>();
		CreateMap<Folder, Application.Models.Folder>();
		CreateMap<File, Application.Models.File>();
		CreateMap<FileDetailsView, Application.Models.FileDetails>();
		CreateMap<UserFileView, Application.Models.UserUploads>();
		CreateMap<UsedStorageStatistics, Application.Models.CompanyStorageStatistics>();
		CreateMap<UploadsByDayStatistics, Application.Models.UploadsByDayStatistics>();
	}
}
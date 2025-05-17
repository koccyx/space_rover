using AutoMapper;
using Model.Models;
using space_rovers.Models.User;
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
	}
}
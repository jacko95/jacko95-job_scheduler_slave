using AutoMapper;
using Master.Models;
using Master.ViewModels;

namespace Master.Data
{
    public class MasterMappingProfile : Profile
    {
        public MasterMappingProfile()
        {
            CreateMap<SiteUser, SiteUserViewModel>();
            CreateMap<SiteUserViewModel, SiteUser>();
        }
    }
}

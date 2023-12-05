using AutoMapper;
using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;

namespace FeeMgmBackend.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig() { 
            CreateMap<UserDto, User>().ReverseMap(); 
            CreateMap<FineDto, Fine>().ReverseMap();
        }
    }
}

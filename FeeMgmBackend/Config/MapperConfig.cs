using AutoMapper;
using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;
using FeeMgmBackend.Models;

namespace FeeMgmBackend.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<MemberDto, Member>().ReverseMap();
            CreateMap<FineDto, Fine>().ReverseMap();
            CreateMap<PaymentDto, Payment>().ReverseMap();
            CreateMap<AuthUserDto, ApplicationUser>().ReverseMap();
        }
    }
}

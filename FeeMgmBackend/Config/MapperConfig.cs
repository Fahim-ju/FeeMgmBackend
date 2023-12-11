using AutoMapper;
using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;

namespace FeeMgmBackend.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<MemberDto, Member>().ReverseMap();
            CreateMap<FineDto, Fine>().ReverseMap();
            CreateMap<PaymentDto, Payment>().ReverseMap();
        }
    }
}

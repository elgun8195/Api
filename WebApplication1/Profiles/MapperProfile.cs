using AutoMapper;
using WebApplication1.Data;
using WebApplication1.DTO;

namespace WebApplication1.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductReturnDto>()
                .ForMember(ds => ds.Image, map => map.MapFrom(sr => "https://localhost:44358/img/"+sr.Image));
        }
    }
}

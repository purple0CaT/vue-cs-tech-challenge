using ApiCore.Clients.Models;
using ApiCore.Models;
using AutoMapper;

namespace ApiCore.Clients.MappingProfiles;

public class StarWarsMappingProfile : Profile
{
    public StarWarsMappingProfile()
    {
        CreateMap<StarWarsApiPeopleResponse, Character>()
            .ForMember(dest => dest.HairColor, opt => opt.MapFrom(src => src.Hair_Color))
            .ForMember(dest => dest.SkinColor, opt => opt.MapFrom(src => src.Skin_Color))
            .ForMember(dest => dest.EyeColor, opt => opt.MapFrom(src => src.Eye_Color))
            .ForMember(dest => dest.BirthYear, opt => opt.MapFrom(src => src.Birth_Year));
    }
}

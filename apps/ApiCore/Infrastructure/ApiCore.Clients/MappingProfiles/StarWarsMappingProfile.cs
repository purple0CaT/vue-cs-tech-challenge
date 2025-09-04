using ApiCore.Clients.Models;
using ApiCore.Models;

using AutoMapper;

namespace ApiCore.Clients.MappingProfiles;

public class StarWarsMappingProfile : Profile {
	public StarWarsMappingProfile() {
		CreateMap<StarWarsApiPeopleResponse, Character>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(src => ExtractIdFromUrl(src.Url)))
			.ForMember(dest => dest.HairColor, opt => opt.MapFrom(src => src.Hair_Color))
			.ForMember(dest => dest.SkinColor, opt => opt.MapFrom(src => src.Skin_Color))
			.ForMember(dest => dest.EyeColor, opt => opt.MapFrom(src => src.Eye_Color))
			.ForMember(dest => dest.BirthYear, opt => opt.MapFrom(src => src.Birth_Year));
	}

	private static string ExtractIdFromUrl(string url) {
		if (string.IsNullOrWhiteSpace(url))
			return 0.ToString();

		var segments = url.TrimEnd('/').Split('/');
		if (segments.Length > 0 && int.TryParse(segments[^1], out var id)) {
			return id.ToString();
		}

		return 0.ToString();
	}
}

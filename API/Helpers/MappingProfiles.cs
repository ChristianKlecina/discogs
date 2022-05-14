using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Track, TrackToReturnDto>()
            .ForMember(d => d.PictureUrl, o => o.MapFrom<TrackUrlResolver>())
            .ForMember(x => x.Producer, o =>o.MapFrom(s => s.Producer.ArtistName))
            .ForMember(x => x.Medium, o =>o.MapFrom(s => s.Medium.MediumName))
            .ForMember(x => x.Label, o =>o.MapFrom(s => s.Label.Name))
            .ForMember(x => x.Genre, o =>o.MapFrom(s => s.Genre.GenreName));

        CreateMap<AdminRegisterDto, User>();
        CreateMap<User, User>();
        CreateMap<TrackToReturnDto, Track>()
            .ForMember(x => x.Genre, o => o.MapFrom(p => p.Genre));
        CreateMap<Track, Track>();
    }
}
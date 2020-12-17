using AutoMapper;

namespace GloboTicket.Services.EventCatalog.Profiles
{
    public class EventProfile: Profile
    {
        public EventProfile()
        {
            CreateMap<Entities.Event, Models.EventDto>()
                .ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.VenueName, opts => opts.MapFrom(src => src.Venue.Name))
                .ForMember(dest => dest.VenueCity, opts => opts.MapFrom(src => src.Venue.City))
                .ForMember(dest => dest.VenueCountry, opts => opts.MapFrom(src => src.Venue.Country));
        }
    }
}

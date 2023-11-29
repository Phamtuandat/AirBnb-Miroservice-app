using AutoMapper;

namespace PropertyApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SaveHouseholder, Householder>();
            CreateMap<SaveImageDto, Media>();
            CreateMap<SavePropertyDto, Property>();
            CreateMap<SaveTypeDto, PlaceType>();
            CreateMap<SaveReviewDto, Review>();
        }
    }
}

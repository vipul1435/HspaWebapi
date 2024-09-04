using AutoMapper;
using webApi.Dtos;
using webApi.Modals;

namespace webApi.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // It map only city to CityDto 
            // If we want to map citydto to city the we have to create another map
            // like CreateMap<CityDto, City>();
            //CreateMap<City, CityDto>();


            //but while create two map-> we can use reverse function for vice versa mapping

            CreateMap<City, CityDto>().ReverseMap();

            CreateMap<Property, PropertyListDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City.Name))
                .ForMember(d => d.Country, opt => opt.MapFrom(s => s.City.Country))
                .ForMember(d => d.FunrnishedType, opt => opt.MapFrom(s => s.FunrnishedType.Name))
                .ForMember(d => d.PropertyType, opt => opt.MapFrom(s => s.PropertyType.Name))
                .ForMember(d => d.PhotoUrl, opt => opt.MapFrom(s => s.Photos.FirstOrDefault(p => p.IsPrimary).ImageUrl));


            CreateMap<Property, PropertyDetailDto>()
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City.Name))
                .ForMember(d => d.Country, opt => opt.MapFrom(s => s.City.Country))
                .ForMember(d => d.FunrnishedType, opt => opt.MapFrom(s => s.FunrnishedType.Name))
                .ForMember(d => d.PropertyType, opt => opt.MapFrom(s => s.PropertyType.Name));

            CreateMap<FurnishedTypeDto, FurnishedType>().ReverseMap();
            CreateMap<PropertyTypeDto, PropertyType>().ReverseMap();

            CreateMap<Property, PropertyReqDto>().ReverseMap();

            CreateMap<PhotoDto, Photo>().ReverseMap();
        }
    }
}

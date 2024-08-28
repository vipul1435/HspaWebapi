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
        }
    }
}

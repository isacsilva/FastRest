using AutoMapper;
using Core;
using FastRest.Models;

namespace FastRest.Mappers
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<RestaurantModel, Restaurant>().ReverseMap();
        }
    }
}

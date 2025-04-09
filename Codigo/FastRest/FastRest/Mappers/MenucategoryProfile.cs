using AutoMapper;
using Core;
using FastRest.Models;

namespace FastRest.Mappers
{
    public class MenucategoryProfile : Profile
    {
        public MenucategoryProfile()
        {
            CreateMap<MenucategoryModel, Menucategory>().ReverseMap();
        }
    }
}

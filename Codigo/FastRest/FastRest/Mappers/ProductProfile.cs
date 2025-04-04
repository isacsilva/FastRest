using AutoMapper;
using FastRest.Models;
using Core;

namespace FastRest.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile() { 
            CreateMap<ProductModel, Product>().ReverseMap();
        }
    }
}

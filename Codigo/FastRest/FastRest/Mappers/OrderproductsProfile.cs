using AutoMapper;
using Core;
using FastRest.Models;

namespace FastRest.Mappers
{
    public class OrderproductsProfile : Profile
    {
        public OrderproductsProfile()
        {
            CreateMap<OrderproductsModel, Orderproducts>().ReverseMap();
        }
    }
}

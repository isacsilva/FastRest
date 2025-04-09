using AutoMapper;
using Core;
using FastRest.Models;

namespace FastRest.Mappers
{
    public class OrdertableProfile : Profile
    {
        public OrdertableProfile()
        {
            CreateMap<OrdertableModel, Ordertable>().ReverseMap();
        }
    }
}

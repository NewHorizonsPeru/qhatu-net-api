using AutoMapper;
using Domain.MainModule.Entities;

namespace Application.MainModule.DTO.Mappings
{
    public class EntityToDtoMappingProfile : Profile
    {
        public EntityToDtoMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(d => d.Id, src => src.MapFrom(m => m.UserId))
                .ForMember(d => d.Username, src => src.MapFrom(m => m.Email));
            CreateMap<Category, CategoryDto>()
                .ForMember(d => d.Id, src => src.MapFrom(m => m.CategoryId));
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Id, src => src.MapFrom(m => m.ProductId));
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDetail, OrderDetailDto>();
        }
    }
}
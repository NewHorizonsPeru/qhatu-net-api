using AutoMapper;
using Domain.MainModule.Entities;

namespace Application.MainModule.DTO.Mappings
{
    public class DtoToEntityMappingProfile : Profile
    {
        public DtoToEntityMappingProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(d => d.UserId, src => src.MapFrom(m => m.Id))
                .ForMember(d => d.Email, src => src.MapFrom(m => m.Username));

            CreateMap<CategoryDto, Category>()
                .ForMember(d => d.CategoryId, src => src.MapFrom(m => m.Id));
            CreateMap<ProductDto, Product>()
                .ForMember(d => d.ProductId, src => src.MapFrom(m => m.Id));
            CreateMap<OrderDto, Order>();
            CreateMap<OrderDetailDto, OrderDetail>();
        }
    }
}
using AutoMapper;
using SampleWebApi.DTOs;
using SampleWebApi.Entities;

namespace SampleWebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserResponse>();
            CreateMap<UserCreateRequest, User>();
            CreateMap<UserUpdateRequest, User>();

            // Order mappings
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : null))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));

            CreateMap<OrderCreateRequest, Order>();
            CreateMap<OrderUpdateRequest, Order>();

            // Category mappings
            CreateMap<Category, CategoryResponse>()
                .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.Name : null));

            CreateMap<CategoryCreateRequest, Category>();
            CreateMap<CategoryUpdateRequest, Category>();

            // Product mappings
            CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null));

            CreateMap<ProductCreateRequest, Product>();
            CreateMap<ProductUpdateRequest, Product>();

            // OrderItem mappings
            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : null))
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.Order != null ? src.Order.OrderNumber : null));

            CreateMap<OrderItemCreateRequest, OrderItem>();

            // Role mappings
            CreateMap<Role, RoleResponse>();
            CreateMap<RoleCreateRequest, Role>();
            CreateMap<RoleUpdateRequest, Role>();

            // UserRole mappings - 1つだけ定義
            CreateMap<UserRole, UserRoleResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : null))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : null));

            CreateMap<UserRoleCreateRequest, UserRole>();
        }
    }
}

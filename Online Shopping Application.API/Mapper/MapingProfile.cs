
namespace Online_Shopping_Application.API.Mapper
{
    public class MapingProfile : Profile
    {
        public MapingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap(); 
            CreateMap<Role, RolesDTO>().ReverseMap();
            CreateMap<UserLogin, UserLoginDTO>().ReverseMap(); 
            CreateMap<UserRole, UserRolesDTO>().ReverseMap();
        }
    }
}
